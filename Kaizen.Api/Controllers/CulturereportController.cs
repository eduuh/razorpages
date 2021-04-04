using System.Collections.Generic;
using AutoMapper;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models.Dto;
using Kaizen.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers
{
    [Route("culture/")]
    public class CulturereportController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        private readonly IMapper _mapper;
        public CulturereportController(IUnitofWork unitofwork, IMapper mapper)
        {
            this._mapper = mapper;
            this._unitofwork = unitofwork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _unitofwork.CultureReport.GetAll();
            return Json(new { data = _mapper.Map<IEnumerable<CultureReport>, IEnumerable<CultureReportDto>>(data) });
        }

        [HttpPost]
        public IActionResult Post([FromBody]CultureReportDto cultureReport)
        {
            var culreport = _mapper.Map<CultureReportDto, CultureReport>(cultureReport);
            _unitofwork.CultureReport.Add(culreport);
            _unitofwork.Save();
            return Ok();
        }
    }
}