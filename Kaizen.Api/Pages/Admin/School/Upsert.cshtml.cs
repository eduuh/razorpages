using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UploadandDowloadService.Pages.Admin.School
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitofWork _UnitofWork;

        public UpsertModel(IUnitofWork unitofwork)
        {
            _UnitofWork = unitofwork;
        }

        public Kaizen.Models.School SchoolObj { get; set; }
        public IActionResult OnGet(string id)
        {
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
                _UnitofWork.School.Add(SchoolObj);
            }
            else
            {
                _UnitofWork.School.Update(SchoolObj);
            }
            _UnitofWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
