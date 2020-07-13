using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Sms.V20190711;
using TencentCloud.Sms.V20190711.Models;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Sms;

namespace VNext.Sms
{
    [Dependency(ReplaceServices = true)]
    public class TencentCloudSmsSender : ISmsSender,Volo.Abp.DependencyInjection.ITransientDependency
    {
        private TencentCloudSmsProviderConfiguration _tencentCloudSmsProviderConfiguration;
        private TencentCloudSmsOptions _smsOptions;

        public TencentCloudSmsSender(IOptions<TencentCloudSmsOptions> smsOptions)
        {
            //_tencentCloudSmsProviderConfiguration = tencentCloudSmsProviderConfiguration;
            _smsOptions = smsOptions.Value;
        }
        //https://github.com/TencentCloud/tencentcloud-sdk-dotnet/blob/master/TencentCloudExamples/SendSms.cs
        public async Task SendAsync(SmsMessage smsMessage)
        {
            if (smsMessage is TencentCloudSmsMessage)
            {
                await TencentCloudSmsMessageAsync(smsMessage as TencentCloudSmsMessage);
            }
        }
        
        protected async Task TencentCloudSmsMessageAsync(TencentCloudSmsMessage smsMessage)
        {
            /* 实例化一个请求对象，根据调用的接口和实际情况，可以进一步设置请求参数
                    * 你可以直接查询SDK源码确定SendSmsRequest有哪些属性可以设置
                    * 属性可能是基本类型，也可能引用了另一个数据结构
                    * 推荐使用IDE进行开发，可以方便的跳转查阅各个接口和数据结构的文档说明 */
            SendSmsRequest req = new SendSmsRequest();

            req.SmsSdkAppid =_smsOptions.AppId;
            /* 短信签名内容: 使用 UTF-8 编码，必须填写已审核通过的签名，签名信息可登录 [短信控制台] 查看 */
            req.Sign = string.IsNullOrEmpty(smsMessage.Sign)? _smsOptions.Sign : smsMessage.Sign;
            /* 短信码号扩展号: 默认未开通，如需开通请联系 [sms helper] */
            //req.ExtendCode = "x";
            /* 国际/港澳台短信 senderid: 国内短信填空，默认未开通，如需开通请联系 [sms helper] */
            //req.SenderId = "";
            /* 用户的 session 内容: 可以携带用户侧 ID 等上下文信息，server 会原样返回 */
            //req.SessionContext = "";
            /* 下发手机号码，采用 e.164 标准，+[国家或地区码][手机号]
             * 示例如：+8613711112222， 其中前面有一个+号 ，86为国家码，13711112222为手机号，最多不要超过200个手机号*/
            req.PhoneNumberSet = new String[] { smsMessage.PhoneNumber };
            /* 模板 ID: 必须填写已审核通过的模板 ID。模板ID可登录 [短信控制台] 查看 */
            req.TemplateID = smsMessage.TemplateID;
            /* 模板参数: 若无模板参数，则设置为空*/
            req.TemplateParamSet =smsMessage.TemplateParamSet.Split(";");

            Credential cred = new Credential
            {
                SecretId = _smsOptions.SecretId,
                SecretKey = _smsOptions.SecretKey
            };
            ClientProfile clientProfile = new ClientProfile();
            HttpProfile httpProfile = new HttpProfile();
            httpProfile.Timeout = 60;
            httpProfile.Endpoint = "sms.tencentcloudapi.com";
            clientProfile.HttpProfile = httpProfile;
            SmsClient client = new SmsClient(cred, "ap-guangzhou", clientProfile);
            //return client;

            SendSmsResponse resp = await client.SendSms(req);

            //TODO:记录服务器发送信息

            // 输出json格式的字符串回包
            Console.WriteLine(AbstractModel.ToJsonString(resp));
        }




        public static SmsClient GetClient()
        {
            Credential cred = new Credential
            {
                SecretId = "xxx",
                SecretKey = "xxx"
            };
            SmsClient client = new SmsClient(cred, "ap-guangzhou");
            return client;
        }
    }
}
