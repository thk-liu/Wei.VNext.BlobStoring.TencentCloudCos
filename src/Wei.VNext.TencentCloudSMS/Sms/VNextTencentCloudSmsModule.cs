using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;

namespace VNext.TencentCloudSMS
{
    [DependsOn(typeof(Volo.Abp.Sms.AbpSmsModule),typeof(AbpBlobStoringModule))]
    public class VNextTencentCloudSmsModule : AbpModule
    {

    }
}
