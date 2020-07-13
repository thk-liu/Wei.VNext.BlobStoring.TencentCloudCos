using System;
using System.Collections.Generic;
using System.Text;

namespace VNext.Sms
{
    public class TencentCloudSmsProviderConfigurationNames
    {
        /// <summary>
        /// 开发者访问 COS 服务时拥有的用户维度唯一资源标识，用以标识资源
        /// </summary>
        public const string AppId = "TencentCloud.AppId";
        /// <summary>
        /// 开发者拥有的项目身份识别 ID，用以身份认证
        /// </summary>
        public const string SecretId = "TencentCloud.SecretId";
        /// <summary>
        /// 开发者拥有的项目身份密钥,用以身份认证
        /// </summary>
        public const string SecretKey = "TencentCloud.SecretKey";
        /// <summary>
        /// 存储桶所在的地域
        /// </summary>
        public const string Region = "TencentCloud.Region";
        /// <summary>
        /// 默认签名
        /// </summary>
        public const string Sign = "TencentCloud.Sign";
    }
}
