using System.Collections.Generic;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaizen.Api.Pages.School.Profiles

{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofwork;

        public IndexModel(IUnitofWork unitofwork)
        {
            this._unitofwork = unitofwork;

        }
        public IEnumerable<AppUser> SchoolUsers { get; set; }
        public void OnGet(string id)
        {
            SchoolUsers = _unitofwork.AppUser.GetAll(u => u.SchoolId == id, null, "School");
        }
    }
}
