本项目已不在支持更新请移步至新的存储库[Wei.Abp](https://github.com/thk-liu/Wei.Abp).

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


# Wei.VNext.TencentCloudSms
abp vnext 腾讯云短信发送支持

使用方法 在 ```public override void ConfigureServices(ServiceConfigurationContext context)``` 中

```
Configure<TencentCloudSmsOptions>(options => {
                options.AppId = "";
                options.SecretKey = "";
                options.SecretId = "";
                options.Region = "ap-guangzhou";
            });
```

发送方法
```
 await _sms.SendAsync(new TencentCloudSmsMessage("+8618588688087", "广州安华磨具") { Sign = "广州安华磨具", TemplateID = "656176", TemplateParamSet = "德邦物流;20234234" });
 ```
