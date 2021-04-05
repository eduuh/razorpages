using System.Collections.Generic;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UploadandDowloadService.Pages.Admin.Content
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork unitofWork;

        public IEnumerable<Kaizen.Models.Content> Contents { get; set; }

        public IndexModel(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }
        public void OnGet()
        {
            Contents = unitofWork.Content.GetAll();
        }
    }
}
