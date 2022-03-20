using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft;

namespace CHC.ToastrNotify
{
    public class NotifyBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public NotifyBuilder(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }



        /// <summary>
        /// 加入訊息到佇列
        /// </summary>
        /// <param name="type"> NotifyType - 訊息類型</param>
        /// <param name="message">訊息</param>
        /// <param name="title">標題(可不填寫)</param>
        public void Append(NotifyTypes type, string message, string? title)
        {

            List<NotifyByToastr> notifys;
            var httpContext = _httpContextAccessor.HttpContext;
            var temp = _tempDataDictionaryFactory.GetTempData(httpContext);
            var serializeObjextJson = temp[NotifyByToastr.KEY] as string;
            if (serializeObjextJson != null)
            {
                notifys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NotifyByToastr>>(serializeObjextJson) ?? new List<NotifyByToastr>();

                notifys.Add(Builber(type, message, title ?? String.Empty));
            }
            else
            {
                notifys = new List<NotifyByToastr>();
                notifys.Add(Builber(type, message, title ?? String.Empty));
            }

            temp[NotifyByToastr.KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notifys);
        }

        /// <summary>
        /// 建立 NotifyByToastr
        /// </summary>
        /// <param name="type">NotifyType - 訊息類型</param>
        /// <param name="message">訊息</param>
        /// <param name="title">標題</param>
        /// <returns>NotifyByToastr</returns>
        private NotifyByToastr Builber(NotifyTypes type, string message, string title = null)
        {
            var result = new NotifyByToastr() { title = title, message = message };

            switch (type)
            {
                case NotifyTypes.Success:
                    result.type = "success";
                    break;
                case NotifyTypes.Info:
                    result.type = "info";
                    break;
                case NotifyTypes.Error:
                    result.type = "error";
                    break;
                case NotifyTypes.Warning:
                    result.type = "warning";
                    break;
            }

            return result;
        }
    }
}
