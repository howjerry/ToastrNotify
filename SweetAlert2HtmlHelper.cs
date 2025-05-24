using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Collections.Generic;
using System.Text.Encodings.Web; // For JavaScriptEncoder

namespace CHC.AspNetCore.SweetAlert2Notify
{
    public static class SweetAlert2HtmlHelper
    {
        public static IHtmlContent RenderSweetAlert2Notifications(this IHtmlHelper htmlHelper)
        {
            var notifications = htmlHelper.ViewContext.HttpContext.Items[SweetAlertNotifier.NotifyKey] as List<SweetAlert2Message>;

            if (notifications == null || notifications.Count == 0)
            {
                return HtmlString.Empty;
            }

            var script = new StringBuilder();
            script.AppendLine("<script>");

            foreach (var item in notifications)
            {
                string escapedMessage = JavaScriptEncoder.Default.Encode(item.message);
                string titleOption = "";
                if (!string.IsNullOrEmpty(item.title))
                {
                    titleOption = $"text: '{JavaScriptEncoder.Default.Encode(item.title)}',";
                }

                script.AppendFormat(@"Swal.fire({{ toast: true, position: 'top-end', icon: '{0}', title: '{1}', {2} showConfirmButton: false, timer: 3000, timerProgressBar: true }});",
                    item.type,          // {0} icon type (success, info, etc.)
                    escapedMessage,     // {1} main message, already escaped
                    titleOption         // {2} optional 'text: "title",' part, already escaped if title existed
                );
                script.AppendLine(); 
            }
            
            // Clear notifications from HttpContext.Items after rendering to prevent re-display on refresh
            htmlHelper.ViewContext.HttpContext.Items.Remove(SweetAlertNotifier.NotifyKey);

            script.AppendLine("</script>");
            return new HtmlString(script.ToString());
        }
    }
}
