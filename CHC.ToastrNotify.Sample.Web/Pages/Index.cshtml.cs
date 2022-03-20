using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CHC.ToastrNotify;

namespace CHC.ToastrNotify.Sample.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly NotifyBuilder notifyBuilder;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(IHttpContextAccessor httpContextAccessor, NotifyBuilder notifyBuilder)
        {
            _httpContextAccessor = httpContextAccessor;
            this.notifyBuilder = notifyBuilder;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            notifyBuilder.Append(NotifyTypes.Success, "Message.....", "Title");
            return Redirect("~/");
        }
    }
}