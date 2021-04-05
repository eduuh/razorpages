using System.Collections.Generic;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UploadandDowloadService.Pages.Admin.Incidence
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofwork;

        public IndexModel(IUnitofWork unitofwork)
        {
            this._unitofwork = unitofwork;
        }
        public IEnumerable<CultureReport> IncidenceReport { get; set; }
        public void OnGet()
        {
            IncidenceReport = new List<CultureReport>();
            IncidenceReport = _unitofwork.CultureReport.GetAll();
        }
    }
}
