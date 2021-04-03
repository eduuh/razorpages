using Kaizen.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers
{
    public class ClassController : Controller
    {
        private readonly IUnitofWork unitofWork;

        public ClassController(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = unitofWork.Class.GetAll() });
        }

        // [HttpPost]
        // public IActionResult Create(C)
        // {
        //     return Json(new { data = unitofWork.Class.GetAll() });
        // }
    }
}