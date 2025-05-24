using Microsoft.AspNetCore.Http; // Added
using System.Collections.Generic;
// using System.Text; // No longer needed in this file
// using System.Web; // Removed
// using System.Web.Mvc; // Removed

namespace CHC.AspNetCore.SweetAlert2Notify
{
    public static class SweetAlertNotifier // Renamed
    {
        internal const string NotifyKey = "SweetAlert2_Notify_Key"; // Changed KEY to be internal const and more specific

        public static void Append(HttpContext httpContext, NotifyType type, string message, string title = null) // Signature changed
        {
            if (httpContext == null)
            {
                // Or throw ArgumentNullException, depending on desired behavior if HttpContext is not available
                return; 
            }

            List<SweetAlert2Message> notifications;
            if (httpContext.Items.ContainsKey(NotifyKey) && httpContext.Items[NotifyKey] is List<SweetAlert2Message> existingNotifications)
            {
                notifications = existingNotifications;
            }
            else
            {
                notifications = new List<SweetAlert2Message>();
            }
            
            notifications.Add(BuildMessage(type, message, title));
            httpContext.Items[NotifyKey] = notifications;
        }

        private static SweetAlert2Message BuildMessage(NotifyType type, string message, string title = null) // Renamed
        {
            var result = new SweetAlert2Message() { title = title, message = message };

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
        // The old Notify() HtmlHelper method is removed.
    }
}
