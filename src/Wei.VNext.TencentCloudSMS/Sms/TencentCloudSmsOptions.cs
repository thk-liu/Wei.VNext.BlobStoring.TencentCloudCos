using System;
using System.Collections.Generic;
using System.Text;

namespace VNext.Sms
{
    public class TencentCloudSmsOptions
    {
        public string AppId
        {
            get;
            set;
        }
        public string SecretId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string SecretKey
        {
            get;
            set;
        }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string Region
        {
            get;
            set;
        }
        /// <summary>
        /// 默认签名
        /// </summary>
        public string Sign
        {
            get;
            set;
        }
    }
}
