using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.BlobStoring;

namespace VNext.Sms
{
    public class TencentCloudSmsProviderConfiguration:Volo.Abp.DependencyInjection.ITransientDependency
    {
        public string AppId
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudSmsProviderConfigurationNames.AppId);
            set => _containerConfiguration.SetConfiguration(TencentCloudSmsProviderConfigurationNames.AppId, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }
        public string SecretId
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudSmsProviderConfigurationNames.SecretId);
            set => _containerConfiguration.SetConfiguration(TencentCloudSmsProviderConfigurationNames.SecretId, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }
        /// <summary>
        /// 
        /// </summary>
        public string SecretKey
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudSmsProviderConfigurationNames.SecretKey);
            set => _containerConfiguration.SetConfiguration(TencentCloudSmsProviderConfigurationNames.SecretKey, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string Region
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudSmsProviderConfigurationNames.Region);
            set => _containerConfiguration.SetConfiguration(TencentCloudSmsProviderConfigurationNames.Region, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }
        /// <summary>
        /// 默认签名
        /// </summary>
        public string Sign
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudSmsProviderConfigurationNames.Sign);
            set => _containerConfiguration.SetConfiguration(TencentCloudSmsProviderConfigurationNames.Sign, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public TencentCloudSmsProviderConfiguration(BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}
