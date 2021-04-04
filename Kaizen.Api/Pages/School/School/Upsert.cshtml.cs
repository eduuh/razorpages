using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaizen.Api.Pages.School.School
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitofWork _UnitofWork;
        private readonly UserManager<AppUser> _userManager;

        public UpsertModel(IUnitofWork unitofwork, UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
            _UnitofWork = unitofwork;
        }

        public Kaizen.Models.School SchoolObj { get; set; }
        public IActionResult OnGet(string id)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            SchoolObj = new Kaizen.Models.School();
            if (id != null)
            {
                SchoolObj = _UnitofWork.School.GetFirstOrDefault(u => u.Id == id);
                if (SchoolObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost(Kaizen.Models.School SchoolObj)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (SchoolObj.Id == null)
            {
                var user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
                _UnitofWork.School.Add(SchoolObj);
                _UnitofWork.Save();
                SchoolObj = _UnitofWork.School.GetFirstOrDefault(s => s.Name == SchoolObj.Name);
                user.SchoolId = SchoolObj.Id;
                _UnitofWork.Save();
            }
            else
            {
                _UnitofWork.School.Update(SchoolObj);
            }
            return RedirectToPage("./Index");
        }
    }
}
