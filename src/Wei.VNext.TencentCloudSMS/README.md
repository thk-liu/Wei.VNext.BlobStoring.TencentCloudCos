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