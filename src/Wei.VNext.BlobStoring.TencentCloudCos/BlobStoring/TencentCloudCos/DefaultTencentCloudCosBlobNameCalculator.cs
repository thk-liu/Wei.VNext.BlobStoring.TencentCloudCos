using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace VNext.BlobStoring.TencentCloudCos
{
    public class DefaultTencentCloudCosBlobNameCalculator: ITencentCloudBlobNameCalculator, ITransientDependency
    {
        protected ICurrentTenant CurrentTenant { get; }
       
        public DefaultTencentCloudCosBlobNameCalculator(ICurrentTenant currentTenant)
        {
            CurrentTenant = currentTenant;
        }

        public virtual string Calculate(BlobProviderArgs args)
        {
            if (CurrentTenant.Id == null)
                return $"host/{args.BlobName}";
            //优先取租户名称 注意租户是否存在重复
            else if (!string.IsNullOrEmpty(CurrentTenant.Name))
                return $"tenants/{CurrentTenant.Name}/{args.BlobName}";
            else
                return $"tenants/{CurrentTenant.Id.Value.ToString("D")}/{args.BlobName}";

        }
    }
}
