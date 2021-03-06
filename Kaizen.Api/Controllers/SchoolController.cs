using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Kaizen.Models.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper _mapper;
        public readonly IWebHostEnvironment _hostingenvironment;

        public SchoolController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            this._mapper = mapper;
            this.unitofWork = unitofWork;
            _hostingenvironment = hostingEnvironment;
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
        [HttpGet]
        public IActionResult Get()
        {
            var data = unitofWork.School.GetAll();
            return Json(new { data = _mapper.Map<IEnumerable<School>, IEnumerable<SchoolDto>>(data) });
        }

        [HttpGet("names")]
        public ActionResult<IEnumerable<SelectListItem>> GetSchools()
        {
            var schools = unitofWork.School.GetSchooListForDropdown();
            return Json(new { data = schools });
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDto>> GetSchool(string id)
        {
            var school = unitofWork.School.GetFirstOrDefault(s => s.Id == id);
            return _mapper.Map<School, SchoolDto>(school);
        }

        // PUT: api/Schools/5

    }
}