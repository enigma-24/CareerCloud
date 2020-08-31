using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyLocationController : ControllerBase
    {
        private CompanyLocationLogic logic;
        public CompanyLocationController()
        {
            EFGenericRepository<CompanyLocationPoco> repo = new EFGenericRepository<CompanyLocationPoco>();
            logic = new CompanyLocationLogic(repo);
        }

        [HttpGet]
        [Route("location")]
        [ProducesResponseType(typeof(CompanyLocationPoco),200)]
        public IActionResult GetCompanyLocation(Guid id)
        {
            CompanyLocationPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("location")]
        public IActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("location")]
        public IActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("location")]
        public IActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("location")]
        [ProducesResponseType(typeof(List<CompanyLocationPoco>), 200)]
        public IActionResult GetAllCompanyLocation()
        {
            return Ok(logic.GetAll());
        }
    }
}
