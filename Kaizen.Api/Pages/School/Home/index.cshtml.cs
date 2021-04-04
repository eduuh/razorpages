using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaizen.Api.Pages.School.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork unitofwork;

        private readonly UserManager<AppUser> _userManager;

        public IndexModel(IUnitofWork unitofwork, UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
            this.unitofwork = unitofwork;
        }

        public Kaizen.Models.School School { get; set; }

        public void OnGet()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            School = unitofwork.School.GetFirstOrDefault(s => s.Id == user.SchoolId);
        }
    }
}