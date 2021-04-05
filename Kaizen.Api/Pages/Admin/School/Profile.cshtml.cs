using System.Collections.Generic;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UploadandDowloadService.Pages.Admin.School
{
    public class ProfileModel : PageModel
    {
        private readonly IUnitofWork unitofwork;

        public ProfileModel(IUnitofWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        public Kaizen.Models.School School { get; set; }

        public IEnumerable<Kaizen.Models.Class> Classes { get; set; }
        public IEnumerable<Kaizen.Models.AppUser> SchoolUser { get; set; }
        public IActionResult OnGet(string id)
        {
            School = new Kaizen.Models.School();
            if (id != null)
            {
                School = unitofwork.School.GetFirstOrDefault(s => s.Id == id);
                Classes = unitofwork.Class.GetAll(s => s.SchoolId == School.Id);
                SchoolUser = unitofwork.AppUser.GetAll(s => s.SchoolId == School.Id);
                if (School == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }
    }
}
