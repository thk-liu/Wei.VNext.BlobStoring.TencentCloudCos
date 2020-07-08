using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Testing;

namespace Wei.VNext.BlobStoring.QCloud.Cos.Tests
{
    public class VNextBlobStoringTencentCloudCosTestBase : AbpIntegratedTest<VNextBlobStoringTencentCloudCosTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
