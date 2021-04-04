using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaizen.Api.Pages.School.Classes
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofwork;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(IUnitofWork unitofwork, UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
            this._unitofwork = unitofwork;

        }

        public IEnumerable<Kaizen.Models.Class> Classes { get; set; }
        public void OnGet()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            Classes = new List<Kaizen.Models.Class>();
            Classes = _unitofwork.Class.GetAll(f => f.SchoolId == user.SchoolId);

        }
    }
}
