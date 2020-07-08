# Wei.VNext.BlobStoring.TencentCloudCos
abp vnext blob 腾讯云的支持

使用方法 在 ```public override void ConfigureServices(ServiceConfigurationContext context)``` 中

```
var configuration = context.Services.GetConfiguration();
            var appid = configuration["TencentCloud:AppId"];
            var secretKey = configuration["TencentCloud:SecretKey"];
            var secretId = configuration["TencentCloud:SecretId"];
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(action =>
                {
                    action.UseTencentCloudCos(cos =>
                    {
                        cos.AppId = appid;
                        cos.SecretKey = secretKey;
                        cos.SecretId = secretId;
                        //区域建议手动配置
                        cos.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
                    });
                });
            });
```