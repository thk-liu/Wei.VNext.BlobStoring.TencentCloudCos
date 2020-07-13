using COSXML.Common;
using COSXML.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VNext.BlobStoring.TencentCloudCos;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;

namespace Wei.VNext.BlobStoring.QCloud.Cos.Tests
{
 
    [DependsOn(
        typeof(AbpBlobStoringModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule),
        typeof(VNextBlobStoringTencentCloudCosModule)
    )]
    public class VNextBlobStoringTencentCloudCosTestModule: AbpModule
    {
        private const string UserSecretsId = "AKIDseJvOcGcDXaOxFp9l6F8nBI7l9G7w18z";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //正式环境建议从Secrets环境中取值
            //context.Services.ReplaceConfiguration(ConfigurationHelper.BuildConfiguration(builderAction: builder =>
            //{
            //    builder.AddUserSecrets(UserSecretsId);
            //}));
            context.Services.AddTransient<ITencentCloudBlobNameCalculator, DefaultTencentCloudCosBlobNameCalculator>();
            context.Services.AddTransient<IBlobProvider, TencentCloudCosBlobProvider>();

            var configuration = context.Services.GetConfiguration();
            var appid = configuration["TencentCloud:AppId"];
            var secretKey = configuration["TencentCloud:SecretKey"];
            var secretId= configuration["TencentCloud:SecretId"];
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(action =>
                {
                    action.UseTencentCloudCos(cos =>
                    {
                        cos.AppId = appid;
                        cos.SecretKey = secretKey;
                        cos.SecretId = secretId;
                        cos.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
                    });

                });
            });
        }
    }
}
