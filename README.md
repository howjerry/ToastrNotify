# CHC.AspNetCore.SweetAlert2Notify
A helper library for ASP.NET Core (.NET 8 and later) to display server-side generated notifications using the [SweetAlert2](https://sweetalert2.github.io/) library.

## Features
*   Easily append notifications (success, error, warning, info) from your ASP.NET Core controller actions.
*   Renders notifications as SweetAlert2 toast messages.
*   Simple integration with Razor views.

## Installation

Install the NuGet package:
```powershell
Install-Package CHC.AspNetCore.SweetAlert2Notify 
```
*(Note: Ensure you are using version 1.0.0 or later for .NET 8 compatibility. The package ID is `CHC.AspNetCore.SweetAlert2Notify`.)*

## Setup

1.  **Include SweetAlert2 Client-Side Library:**
    You need to include SweetAlert2 in your application's pages. The easiest way is using a CDN. Add this to your main layout file (e.g., `_Layout.cshtml`), typically before the closing `</body>` tag:
    ```html
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    ```
    SweetAlert2's CSS is automatically included with this CDN link. Alternatively, you can install it via npm/yarn and bundle it with your application's assets.

2.  **Ensure HttpContext is available (usually default):**
    This library relies on `HttpContext` to store and retrieve notifications for the current request. In most ASP.NET Core applications (Controllers, Razor Pages), `HttpContext` is readily available. No special setup is typically required for this.

## Usage

1.  **Append Notifications in Your Controller:**
    In your controller action, use the `SweetAlertNotifier.Append()` method to add notifications. You need to pass the current `HttpContext` to it.

    ```csharp
    using CHC.AspNetCore.SweetAlert2Notify; // Add this using statement
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Append notifications by passing the current HttpContext
            SweetAlertNotifier.Append(this.HttpContext, NotifyType.Success, "Welcome!", "You've successfully logged in.");
            SweetAlertNotifier.Append(this.HttpContext, NotifyType.Info, "This is an informational message.");
            
            return View();
        }

        public IActionResult AnotherAction()
        {
            SweetAlertNotifier.Append(this.HttpContext, NotifyType.Error, "Something went wrong!", "Please try again.");
            // If you redirect, notifications stored via HttpContext.Items will be lost.
            // Consider TempData for Post-Redirect-Get scenarios.
            return RedirectToAction("Index"); 
        }
    }
    ```

2.  **Render Notifications in Your Razor View:**
    Call the HTML helper in your Razor view (e.g., `_Layout.cshtml` or a specific view) to render the accumulated notifications. This should be placed *after* the SweetAlert2 script.

    ```html
    @using CHC.AspNetCore.SweetAlert2Notify // Optional: Or fully qualify the helper
    
    <!DOCTYPE html>
    <html>
    <head>
        <!-- ... head content ... -->
    </head>
    <body>
        <!-- ... other body content ... -->

        <!-- Include SweetAlert2 script first -->
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script> 
        
        <!-- Then render notifications -->
        @Html.RenderSweetAlert2Notifications() 
        
        <!-- ... other scripts ... -->
    </body>
    </html>
    ```

## Customization
This library renders basic toast notifications with a default configuration:
*   Position: 'top-end'
*   Timer: 3000ms
*   Progress bar: enabled
*   No confirmation button

SweetAlert2 is highly customizable. If you need different types of alerts (e.g., modals, different icons, input fields) or want to change the appearance/behavior of toasts, please refer to the [official SweetAlert2 documentation](https://sweetalert2.github.io/). You can either modify this library or call SweetAlert2 directly from your client-side JavaScript for advanced scenarios.

## Considerations for Post-Redirect-Get (PRG)
The current version stores notifications in `HttpContext.Items`, which means they are only available for the current request. If you redirect after setting a notification (e.g., after a successful form post), the notification will be lost. For PRG scenarios, notifications should be stored in `TempData`. Future versions of this library may include explicit TempData support or you can implement a TempData-based notifier yourself.
```
