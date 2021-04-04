using Kaizen.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UploadandDowloadService.Pages.Admin.Content
{
    public class TrainingModel : PageModel
    {
        private readonly IUnitofWork unitofwork;

        public TrainingModel(IUnitofWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }

        public Kaizen.Models.Content TrainingObj { get; set; }
        public void OnGet()
        {
            TrainingObj = new Kaizen.Models.Content();
        }
    }
}
