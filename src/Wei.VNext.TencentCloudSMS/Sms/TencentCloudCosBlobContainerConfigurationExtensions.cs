using System;
using System.Collections.Generic;
using System.Text;
using VNext.Sms;
using Volo.Abp.BlobStoring;

namespace VNext.Sms
{
   public static class TencentCloudSmsBlobContainerConfigurationExtensions
    {

        public static TencentCloudSmsProviderConfiguration GetCosConfiguration(
           this BlobContainerConfiguration containerConfiguration)
        {
            return new TencentCloudSmsProviderConfiguration(containerConfiguration);
        }
        /// <summary>
        /// 使用Bolb存储腾讯SMS配置信息
        /// </summary>
        /// <param name="containerConfiguration"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static BlobContainerConfiguration UseTencentCloudSms(
            this BlobContainerConfiguration containerConfiguration,Action<TencentCloudSmsProviderConfiguration> configureAction=null)
        {
            configureAction?.Invoke(new TencentCloudSmsProviderConfiguration(containerConfiguration));

            return containerConfiguration;
        }
    }
}
