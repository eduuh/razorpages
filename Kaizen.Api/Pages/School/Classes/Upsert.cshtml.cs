using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaizen.Api.Pages.School.Classes
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitofWork _unitofwork;
        private readonly UserManager<AppUser> _userManager;

        public Kaizen.Models.Class Class { get; set; }
        public UpsertModel(IUnitofWork unitofwork, UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
            this._unitofwork = unitofwork;
        }
        public IActionResult OnGet(string id)
        {
            Class = new Kaizen.Models.Class();
            if (id != null)
            {
                Class = _unitofwork.Class.GetFirstOrDefault(u => u.Id == id);
                if (Class == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost(Kaizen.Models.Class Class)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            Class.SchoolId = user.SchoolId;
            ;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Class.Id == null)
            {
                _unitofwork.Class.Add(Class);
            }
            else
            {
                _unitofwork.Class.Update(Class);
            }
            _unitofwork.Save();
            return RedirectToPage("./Index");
        }
    }
}
