using Kaizen.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UploadandDowloadService.Pages.Admin.Content
{
    public class CultureModel : PageModel
    {
        private readonly IUnitofWork _unitofwork;

        public CultureModel(IUnitofWork unitofwork)
        {
            this._unitofwork = unitofwork;
        }
        public Kaizen.Models.Content Contentobj { get; set; }
        public IActionResult OnGet(string id)
        {
            Contentobj = new Kaizen.Models.Content();

            if (id != null)
            {
                Contentobj = _unitofwork.Content.GetFirstOrDefault(s => s.Id == id);
                if (Contentobj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost(Kaizen.Models.Content Contentobj)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Contentobj.Id == null)
            {
                _unitofwork.Content.Add(Contentobj);
            }
            else
            {
                _unitofwork.Content.update(Contentobj);
            }
            _unitofwork.Save();
            return RedirectToPage("./Index");
        }
    }
}
