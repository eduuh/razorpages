using Kaizen.DataAccess.Data.Repository.IRepository;
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
        public void OnGet()
        {
            Contentobj = new Kaizen.Models.Content();
        }
    }
}
