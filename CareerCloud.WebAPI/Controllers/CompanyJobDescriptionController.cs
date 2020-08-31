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
    public class CompanyJobDescriptionController : ControllerBase
    {
        private CompanyJobDescriptionLogic logic;
        public CompanyJobDescriptionController()
        {
            EFGenericRepository<CompanyJobDescriptionPoco> repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            logic = new CompanyJobDescriptionLogic(repo);
        }

        [HttpGet]
        [Route("jobdescription")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco),200)]
        public IActionResult GetCompanyJobDescription(Guid id)
        {
            CompanyJobDescriptionPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("jobdescription")]
        public IActionResult PostCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobdescription")]
        public IActionResult PutCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobdescription")]
        public IActionResult DeleteCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("jobdescription")]
        [ProducesResponseType(typeof(List<CompanyJobDescriptionPoco>), 200)]
        public IActionResult GetAllCompanyJobDescription()
        {
            return Ok(logic.GetAll());
        }
    }
}
