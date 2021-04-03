using Kaizen.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : Controller
    {
        private readonly IUnitofWork unitofWork;

        public SchoolController(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = unitofWork.School.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var objectfromdb = unitofWork.School.GetFirstOrDefault(u => u.Id == id);

            if (objectfromdb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            unitofWork.School.Remove(objectfromdb);
            unitofWork.Save();
            return Json(new { success = true, message = "Deleted Succesfully" });
        }
    }
}