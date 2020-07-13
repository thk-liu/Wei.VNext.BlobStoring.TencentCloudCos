using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Testing;

namespace Wei.VNext.TencentCloudSms.Tests
{
    public class VNextBlobStoringTencentCloudSmsTestBase : AbpIntegratedTest<VNextBlobStoringTencentCloudSmsTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
