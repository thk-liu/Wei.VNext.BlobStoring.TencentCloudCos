using COSXML;
using COSXML.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace VNext.BlobStoring.TencentCloudCos
{
    public class TencentCloudClient
    {
        public TencentCloudClient(TencentCloudCosBlobProviderConfiguration configuration)
        {
            //InitCosXml();
            Client = new Lazy<CosXml>(() =>
              {
                  // 初始化 CosXmlConfig
                  CosXmlConfig config = new CosXmlConfig.Builder()
                  .SetConnectionLimit(1024)
                  .SetConnectionTimeoutMs(60000)
                  .SetReadWriteTimeoutMs(40000)
                  .IsHttps(true)
                  .SetAppid(configuration.AppId)
                  .SetDebugLog(true)
                  .SetRegion(configuration.Region)
                  .Build();

                  QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(configuration.SecretId, configuration.SecretKey, 600);

                  ////使用临时密钥， 推荐
                  ////开发者拥有的项目身份识别 临时ID，用以身份认证
                  //string tmpSecretId = "xxxx"; //https://cloud.tencent.com/document/product/436/14048
                  ////开发者拥有的项目身份 临时密钥,用以身份认证
                  //string tmpSecretKey = "xxxx"; //https://cloud.tencent.com/document/product/436/14048
                  ////临时密钥 token
                  //string tmpToken = "xxxx"; //https://cloud.tencent.com/document/product/436/14048
                  ////临时密钥有效截止时间,UNIX 时间戳 
                  //long tmpExpireTime = 1546917879; //https://cloud.tencent.com/document/product/436/14048
                  //QCloudCredentialProvider cosCredentialProvider = new DefaultSessionQCloudCredentialProvider(tmpSecretId, tmpSecretKey, tmpExpireTime, tmpToken);

                  //初始化 CosXmlServer
                  return new CosXmlServer(config, cosCredentialProvider);
              });
        }

        //net sdk 服务类，提供了各个访问cos API 的接口
        public Lazy<CosXml> Client { get; private set;}
      
    }
}
