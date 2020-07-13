using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VNext.Sms;
using Volo.Abp.Sms;
using Xunit;

namespace Wei.VNext.TencentCloudSms.Tests
{
    public class TencentCloudSms_Tests : VNextBlobStoringTencentCloudSmsTestBase
    {
        private readonly ISmsSender _sms;

        public TencentCloudSms_Tests()
        {
            _sms = GetRequiredService<ISmsSender>();
        }
        [Fact]
        public async Task SendTestSms()
        {
            await _sms.SendAsync(new TencentCloudSmsMessage("+8618588688087", "广州安华磨具") { Sign = "广州安华磨具", TemplateID = "656176", TemplateParamSet = "德邦物流;20234234" });
        }
    }
}
