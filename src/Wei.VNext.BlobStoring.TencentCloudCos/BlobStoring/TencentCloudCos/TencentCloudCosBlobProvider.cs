using COSXML;
using COSXML.Model.Object;
using COSXML.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;


namespace VNext.BlobStoring.TencentCloudCos
{
    public class TencentCloudCosBlobProvider : BlobProviderBase, ITransientDependency
    {
        protected ITencentCloudBlobNameCalculator BlobNameCalculator { get; }

        public TencentCloudCosBlobProvider(ITencentCloudBlobNameCalculator cosBlobNameCalculator)
        {
           
            BlobNameCalculator = cosBlobNameCalculator;
        }
        /// <summary>
        /// 保存内容
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task SaveAsync(BlobProviderSaveArgs args)
        {
            var blobName = BlobNameCalculator.Calculate(args);

            if (!args.OverrideExisting && await BlobExistsAsync(args, blobName))
            {
                throw new BlobAlreadyExistsException($"Saving BLOB '{args.BlobName}' does already exists in the container '{GetContainerName(args)}'! Set {nameof(args.OverrideExisting)} if it should be overwritten.");
            }

            await CreateContainerIfNotExists(args);

            byte[] data = new byte[args.BlobStream.Length];
            await args.BlobStream.ReadAsync(data, 0, data.Length);

            var request = new PutObjectRequest(GetBucket(args), blobName, data);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            var result = GetClient(args).PutObject(request);
  
        }

        protected virtual CosXml GetClient(BlobProviderArgs args)
        {
            var configuration = args.Configuration.GetCosConfiguration();
            var blobServiceClient = new TencentCloudClient(configuration);
            return blobServiceClient.Client.Value;
        }

        protected virtual string GetBucket(BlobProviderArgs args)
        {
            var configuration = args.Configuration.GetCosConfiguration();
            //腾讯云存储桶后面必须跟上appid
            return $"{args.ContainerName}-{configuration.AppId}";
        }

        public override async Task<bool> DeleteAsync(BlobProviderDeleteArgs args)
        {
            var blobName = BlobNameCalculator.Calculate(args);

            if (await BlobExistsAsync(args, blobName))
            {
                var request = new DeleteObjectRequest(args.ContainerName, blobName);

                try
                {
                    var result = GetClient(args).DeleteObject(request);
                    return true;
                }
                catch (Exception ee)
                {
                    return false;
                    // throw;
                }
            }
            return false;
        }

        public override async Task<bool> ExistsAsync(BlobProviderExistsArgs args)
        {
            var blobName = BlobNameCalculator.Calculate(args);

            return await BlobExistsAsync(args, blobName);
        }

        public override async Task<Stream> GetOrNullAsync(BlobProviderGetArgs args)
        {
            var blobName = BlobNameCalculator.Calculate(args);

            if (!await BlobExistsAsync(args, blobName))
            {
                return null;
            }
            var result = GetClient(args).GetObject(new GetObjectBytesRequest(args.ContainerName, blobName));

            var memoryStream = new MemoryStream();
            await memoryStream.WriteAsync(result.content, 0, result.content.Length);

            return memoryStream;
        }

        //protected virtual CosXml GetBlobClient(BlobProviderArgs args, string blobName)
        //{
        //    //return QCloudServer.CosXml.Value;
        //    //   var blobContainerClient = GetBlobContainerClient(args);
        //    //return blobContainerClient.GetBlobClient(blobName);
        //}

        //protected virtual BlobContainerClient GetBlobContainerClient(BlobProviderArgs args)
        //{
        //    var configuration = args.Configuration.GetAzureConfiguration();
        //    var blobServiceClient = new BlobServiceClient(configuration.ConnectionString);
        //    return blobServiceClient.GetBlobContainerClient(GetContainerName(args));
        //}
        /// <summary>
        /// 创建存储桶
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual async Task CreateContainerIfNotExists(BlobProviderArgs args)
        {
            try
            {
                var result = GetClient(args).HeadBucket(new COSXML.Model.Bucket.HeadBucketRequest(GetContainerName(args)));
            }
            catch (COSXML.CosException.CosServerException ee)
            {
                if (ee.statusCode == 404)
                {
                    //指定的bucket容器
                    //默认是私有化的
                    var putresult = GetClient(args).PutBucket(new COSXML.Model.Bucket.PutBucketRequest(args.ContainerName));
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        /// <summary>
        /// 对象存储是否存在
        /// </summary>
        /// <param name="args"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        private async Task<bool> BlobExistsAsync(BlobProviderArgs args, string blobName)
        {
            // Make sure Blob Container exists.
            var containerExists = await ContainerExistsAsync(args);
            if (containerExists == false)
            {
                return false;
            }
            try
            {
                var result = GetClient(args).HeadObject(new HeadObjectRequest(args.ContainerName, blobName));
                return true;
            }
            catch (COSXML.CosException.CosServerException ee)
            {
                return ee.statusCode == 404 ? false : true;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        private static string GetContainerName(BlobProviderArgs args)
        {
            //腾讯云COS存储规则
            //<BucketName-APPID>.cos.<Region>.myqcloud.com
            return args.ContainerName +"-"+args.Configuration.GetCosConfiguration().AppId;
        }
        /// <summary>
        /// 判断是否存在存储桶
        /// </summary>
        /// <param name="blobContainerClient"></param>
        /// <returns></returns>
        private async Task<bool> ContainerExistsAsync(BlobProviderArgs args)
        {
            try
            {
                var result = GetClient(args).HeadBucket(new COSXML.Model.Bucket.HeadBucketRequest(GetContainerName(args)));
                return result.httpCode == 404 ? false : true;
            }
            catch (COSXML.CosException.CosServerException ee)
            {
                return ee.statusCode == 404 ? false : true;
            }
            catch (Exception ee)
            {
                throw ee;
            }
           
        }
    }
}
