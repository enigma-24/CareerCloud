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
    public class CompanyJobEducationController : ControllerBase
    {
        private CompanyJobEducationLogic logic;
        public CompanyJobEducationController()
        {
            EFGenericRepository<CompanyJobEducationPoco> repo = new EFGenericRepository<CompanyJobEducationPoco>();
            logic = new CompanyJobEducationLogic(repo);
        }

        [HttpGet]
        [Route("jobeducation")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]
        public IActionResult GetCompanyJobEducation(Guid id)
        {
            CompanyJobEducationPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("jobeducation")]
        public IActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobeducation")]
        public IActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobeducation")]
        public IActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("jobeducation")]
        [ProducesResponseType(typeof(List<CompanyJobEducationPoco>), 200)]
        public IActionResult GetAllCompanyJobEducation()
        {
            return Ok(logic.GetAll());
        }
    }
}
