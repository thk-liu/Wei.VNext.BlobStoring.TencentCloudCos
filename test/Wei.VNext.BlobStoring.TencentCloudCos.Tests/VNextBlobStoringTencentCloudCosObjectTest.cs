using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using COSXML.Common;
using COSXML.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VNext.BlobStoring.TencentCloudCos;
using Volo.Abp.BlobStoring;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace Wei.VNext.BlobStoring.TencentCloudCos.Tests
{
    /// <summary>
    /// 腾讯云 Cos 对象存储测试
    /// </summary>
    public class VNextBlobStoringTencentCloudCosObjectTest: QCloud.Cos.Tests.VNextBlobStoringTencentCloudCosTestBase
    {
        private readonly IBlobProvider _blob;
        private readonly ITencentCloudBlobNameCalculator _calculator;
        private readonly ICurrentTenant _currentTenant;
        public VNextBlobStoringTencentCloudCosObjectTest()
        {
            _blob = GetRequiredService<IBlobProvider>();
            _calculator = GetRequiredService<ITencentCloudBlobNameCalculator>();
            _currentTenant = GetRequiredService<ICurrentTenant>();
        }

        [Fact]
        public async Task Save_Test()
        {
            var containerName = "b2b";
            var blobName = "appsettings.json";
            var tenantId = Guid.NewGuid();

            var file = new FileStream("appsettings.json", FileMode.Open);

            using (_currentTenant.Change(tenantId, "test"))
            {
                var blod = _calculator.Calculate(GetArgs(containerName, blobName));
                var tt = new BlobProviderSaveArgs(
                            containerName,
                           new BlobContainerConfiguration().UseTencentCloudCos(cos =>
                           {
                               //cos.AppId = "";
                               //cos.SecretId = "";
                               //cos.SecretKey = "";
                               //cos.ContainerName = "";
                               cos.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
                           }),
                            blobName,
                            file,true
                        );
                await _blob.SaveAsync(tt);
            }
        }

        [Fact]
        public void Get_Test()
        {

        }
        private static BlobProviderArgs GetArgs(
           string containerName,
           string blobName)
        {
            return new BlobProviderGetArgs(
                containerName,
                new BlobContainerConfiguration().UseTencentCloudCos(),
                blobName
            );
        }

        [Fact]
        public void Exists_Test()
        {
            var tenantId = Guid.NewGuid();
            using (_currentTenant.Change(tenantId))
            {
                var bu = _calculator.Calculate(GetArgs("my-container", "my-blob"));
                var containerName = "wei";
                var blobName = "123";
                var tt = new BlobProviderExistsArgs(
                containerName,
               new BlobContainerConfiguration().UseTencentCloudCos(cos=> {
                   cos.AppId = "";
                   cos.SecretId = "";
                   cos.SecretKey = "";
                   cos.ContainerName = "wei";
                   cos.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
               }),
                blobName
            );
                var result = _blob.ExistsAsync(tt).GetAwaiter().GetResult(); ;
                Assert.False(result);
            }
        }

        [Fact]
        public async Task Delete_Test()
        {
            var tenantId = Guid.NewGuid();
            using (_currentTenant.Change(tenantId, "test"))
            {
                var containerName = "b2b";
                var blobName = "appsettings.json";
                var args = new BlobProviderDeleteArgs(
                containerName,
               new BlobContainerConfiguration().UseTencentCloudCos(cos =>
               {
                   cos.AppId = "";
                   cos.SecretId = "";
                   cos.SecretKey = "";
                   cos.ContainerName = "wei";
                   cos.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
               }),
                blobName
            );
                var result = await _blob.DeleteAsync(args);
                Assert.False(result);
            }
        }
    }
}
