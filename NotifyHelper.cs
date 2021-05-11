using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CHC.ToastrNotify
{
    public static class NotifyHelper
    {
        /// <summary>
        /// Session Key
        /// </summary>
        private const string KEY = "Notify_Key";
        /// <summary>
        /// 加入訊息到佇列
        /// </summary>
        /// <param name="type"> NotifyType - 訊息類型</param>
        /// <param name="message">訊息</param>
        /// <param name="title">標題(可不填寫)</param>
        public static void Append(NotifyType type, string message, string title = null)
        {
            List<NotifyByToastr> notifys;
            if (HttpContext.Current.Session[KEY] != null)
            {
                notifys = HttpContext.Current.Session[KEY] as List<NotifyByToastr>;
                notifys.Add(Builber(type, message, title));
            }
            else
            {
                notifys = new List<NotifyByToastr>();
                notifys.Add(Builber(type, message, title));
            }

            HttpContext.Current.Session[KEY] = notifys;
        }
        /// <summary>
        /// 建立 NotifyByToastr
        /// </summary>
        /// <param name="type">NotifyType - 訊息類型</param>
        /// <param name="message">訊息</param>
        /// <param name="title">標題</param>
        /// <returns>NotifyByToastr</returns>
        private static NotifyByToastr Builber(NotifyType type, string message, string title = null)
        {
            var result = new NotifyByToastr() { title = title, message = message };

            switch (type)
            {
                case NotifyType.Success:
                    result.type = "success";
                    break;
                case NotifyType.Info:
                    result.type = "info";
                    break;
                case NotifyType.Error:
                    result.type = "error";
                    break;
                case NotifyType.Warning:
                    result.type = "warning";
                    break;
            }

            return result;
        }
        /// <summary>
        /// 依序顯示佇列中的訊息清單
        /// </summary>
        /// <param name="target">HtmlHelper</param>
        /// <returns>HtmlString</returns>
        public static IHtmlString Notify(this HtmlHelper target)
        {
            var notifys = HttpContext.Current.Session[KEY] as List<NotifyByToastr>;
            HttpContext.Current.Session.Remove(KEY);
            if (notifys == null) return null;

            var result = new StringBuilder();
            foreach (var item in notifys)
            {
                if (string.IsNullOrEmpty(item.title))
                    result.Append($@"toastr['{item.type}']('{item.message}');");
                else
                    result.Append($@"toastr['{item.type}']('{item.message}','{item.title}');");
            }

            string script = $"<script>{result.ToString()}</script>";

            return new HtmlString(script);
        }
    }
}
