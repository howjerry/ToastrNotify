using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CHC.ToastrNotify
{
    [HtmlTargetElement("toastr-notify")]
    public class NotifyTagHelper : TagHelper
    {
        private const string IterateOverAttr = "notify-items";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public NotifyTagHelper(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);
            var serializeObjextJson = tempData[NotifyByToastr.KEY] as string;
            List<NotifyByToastr> notifys;
            if (serializeObjextJson != null)
            {
                notifys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NotifyByToastr>>(serializeObjextJson) ?? new List<NotifyByToastr>();

                foreach (var item in notifys)
                {
                    if (string.IsNullOrEmpty(item.title))
                        output.Content.AppendHtml($@"toastr['{item.type}']('{item.message}');");
                    else
                        output.Content.AppendHtml($@"toastr['{item.type}']('{item.message}','{item.title}');");
                }

            }

            output.TagName = "script";
        }
    }
}
