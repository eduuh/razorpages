using System.Collections.Generic;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UploadandDowloadService.Pages.Admin.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;

        public IndexModel(IUnitofWork unitofWork)
        {
            this._unitofWork = unitofWork;
        }

        public IEnumerable<Kaizen.Models.School> Schoolist { get; set; }
        public IEnumerable<Kaizen.Models.AppUser> AppUsers { get; set; }
        public Contact Contact { get; set; }

        public void OnGet()
        {
            Schoolist = _unitofWork.School.GetAll();
            AppUsers = _unitofWork.AppUser.GetAll();
        }
    }
}