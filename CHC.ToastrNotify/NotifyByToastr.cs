using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHC.ToastrNotify
{
    /// <summary>
    /// 傳遞訊息模型
    /// </summary>
    internal class NotifyByToastr
    {
        public const string KEY = "CHC_TOASTR_NOTIFY_ITEMS_KEY";
        /// <summary>
        /// 訊息類型
        /// Success,Info,Warning,Error
        /// </summary>
        public string type { get; set; } = String.Empty;
        /// <summary>
        /// 標題
        /// </summary>
        public string title { get; set; } = String.Empty;
        /// <summary>
        /// 訊息
        /// </summary>
        public string message { get; set; } = String.Empty;
    }
}
