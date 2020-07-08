using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BlobStoring;

namespace VNext.BlobStoring.TencentCloudCos
{
   public static class TencentCloudCosBlobContainerConfigurationExtensions
    {

        public static TencentCloudCosBlobProviderConfiguration GetCosConfiguration(
           this BlobContainerConfiguration containerConfiguration)
        {
            return new TencentCloudCosBlobProviderConfiguration(containerConfiguration);
        }
        /// <summary>
        /// 使用腾讯COS存储
        /// </summary>
        /// <param name="containerConfiguration"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static BlobContainerConfiguration UseTencentCloudCos(
            this BlobContainerConfiguration containerConfiguration,Action<TencentCloudCosBlobProviderConfiguration> configureAction=null)
        {
            containerConfiguration.ProviderType = typeof(TencentCloudCosBlobProvider);
            containerConfiguration.NamingNormalizers.TryAdd<TencentCloudCosBlobNamingNormalizer>();

            configureAction?.Invoke(new TencentCloudCosBlobProviderConfiguration(containerConfiguration));

            return containerConfiguration;
        }
    }
}
