using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.BlobStoring;

namespace VNext.BlobStoring.TencentCloudCos
{
    public class TencentCloudCosBlobProviderConfiguration
    {
        public string AppId
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudCosBlobProviderConfigurationNames.AppId);
            set => _containerConfiguration.SetConfiguration(TencentCloudCosBlobProviderConfigurationNames.AppId, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }
        public string SecretId
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudCosBlobProviderConfigurationNames.SecretId);
            set => _containerConfiguration.SetConfiguration(TencentCloudCosBlobProviderConfigurationNames.SecretId, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }

        public string SecretKey
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudCosBlobProviderConfigurationNames.SecretKey);
            set => _containerConfiguration.SetConfiguration(TencentCloudCosBlobProviderConfigurationNames.SecretKey, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string Region
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudCosBlobProviderConfigurationNames.Region);
            set => _containerConfiguration.SetConfiguration(TencentCloudCosBlobProviderConfigurationNames.Region, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }
        /// <summary>
        /// This name may only contain lowercase letters, numbers, and hyphens, and must begin with a letter or a number.
        /// Each hyphen must be preceded and followed by a non-hyphen character.
        /// The name must also be between 3 and 63 characters long.
        /// If this parameter is not specified, the ContainerName of the <see cref="BlobProviderArgs"/> will be used.
        /// </summary>
        public string ContainerName
        {
            get => _containerConfiguration.GetConfiguration<string>(TencentCloudCosBlobProviderConfigurationNames.ContainerName);
            set => _containerConfiguration.SetConfiguration(TencentCloudCosBlobProviderConfigurationNames.ContainerName, Check.NotNullOrWhiteSpace(value, nameof(value)));
        }

        /// <summary>
        /// Default value: false.
        /// </summary>
        public bool CreateContainerIfNotExists
        {
            get => _containerConfiguration.GetConfigurationOrDefault(TencentCloudCosBlobProviderConfigurationNames.CreateContainerIfNotExists, false);
            set => _containerConfiguration.SetConfiguration(TencentCloudCosBlobProviderConfigurationNames.CreateContainerIfNotExists, value);
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public TencentCloudCosBlobProviderConfiguration(BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}
