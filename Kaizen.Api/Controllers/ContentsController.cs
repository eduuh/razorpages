using System.Collections.Generic;
using AutoMapper;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Kaizen.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace uploaddownloadfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        public ContentsController(IUnitofWork unitofWork, IMapper mapper)
        {
            this._mapper = mapper;
            this._unitofWork = unitofWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _unitofWork.Content.GetAll();
            return Json(new { data = _mapper.Map<IEnumerable<Content>, IEnumerable<ContentDto>>(data) });
        }
    }
}
