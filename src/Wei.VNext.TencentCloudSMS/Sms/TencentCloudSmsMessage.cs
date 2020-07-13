using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Sms;

namespace VNext.Sms
{
    public class TencentCloudSmsMessage : SmsMessage
    {
        public TencentCloudSmsMessage(string phoneNumber, string text) : base(phoneNumber, text)
        {
        }

        /// <summary>
        /// 模板 ID: 必须填写已审核通过的模板 ID。模板ID可登录 [短信控制台] 查看
        /// </summary>
        public string TemplateID { get; set; }

        public string Sign { get; set; }
        /// <summary>
        /// 参数设置 多个参数请用";"分割
        /// </summary>
        public string TemplateParamSet { get; set; }
    }
}
