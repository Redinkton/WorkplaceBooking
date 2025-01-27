using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WorkplaceBooking.Pages
{
    public class ExternalLoginModel : PageModel
    {
        public IActionResult OnPostGoogle()
        {
            var redirectUrl = Url.Page("/Index");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
    }
}