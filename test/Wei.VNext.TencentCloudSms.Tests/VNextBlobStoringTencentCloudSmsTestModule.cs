using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VNext.TencentCloudSMS;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;
using VNext.Sms;
using Microsoft.Extensions.Options;

namespace Wei.VNext.TencentCloudSms.Tests
{
 
    [DependsOn(
        typeof(AbpBlobStoringModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule),
        typeof(VNextTencentCloudSmsModule)
    )]
    public class VNextBlobStoringTencentCloudSmsTestModule: AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //正式环境建议从Secrets环境中取值
            //context.Services.ReplaceConfiguration(ConfigurationHelper.BuildConfiguration(builderAction: builder =>
            //{
            //    builder.AddUserSecrets(UserSecretsId);
            //}));

            var configuration = context.Services.GetConfiguration();
            var appid = configuration["TencentCloud:AppId"];
            var secretKey = configuration["TencentCloud:SecretKey"];
            var secretId = configuration["TencentCloud:SecretId"];

            Configure<TencentCloudSmsOptions>(options => {
                options.AppId = "";
                options.SecretKey = "";
                options.SecretId = "";
                options.Region = "ap-guangzhou";
            });
            
        }
    }
}
