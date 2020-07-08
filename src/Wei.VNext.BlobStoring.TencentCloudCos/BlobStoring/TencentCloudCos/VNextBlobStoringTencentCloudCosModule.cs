using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;

namespace VNext.BlobStoring.TencentCloudCos
{
    [DependsOn(typeof(AbpBlobStoringModule))]
    public class VNextBlobStoringTencentCloudCosModule: AbpModule
    {

    }
}
