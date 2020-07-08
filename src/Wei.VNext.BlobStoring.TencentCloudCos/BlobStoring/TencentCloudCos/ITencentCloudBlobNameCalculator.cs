using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace VNext.BlobStoring.TencentCloudCos
{
    public interface ITencentCloudBlobNameCalculator: ITransientDependency
    {
        string Calculate(BlobProviderArgs args);
    }
}
