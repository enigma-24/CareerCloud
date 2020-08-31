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
    public class CompanyJobController : ControllerBase
    {
        private CompanyJobLogic logic;
        public CompanyJobController()
        {
            EFGenericRepository<CompanyJobPoco> repo = new EFGenericRepository<CompanyJobPoco>();
            logic = new CompanyJobLogic(repo);
        }

        [HttpGet]
        [Route("job")]
        [ProducesResponseType(typeof(CompanyJobPoco),200)]
        public IActionResult GetCompanyJob(Guid id)
        {
            CompanyJobPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("job")]
        public IActionResult PostCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("job")]
        public IActionResult PutCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("job")]
        public IActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("job")]
        [ProducesResponseType(typeof(List<CompanyJobPoco>), 200)]
        public IActionResult GetAllCompanyJob()
        {
            return Ok(logic.GetAll());
        }
    }
}
