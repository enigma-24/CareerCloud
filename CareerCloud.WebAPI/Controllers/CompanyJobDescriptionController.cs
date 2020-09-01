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
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic logic;
        public CompanyJobsDescriptionController()
        {
            EFGenericRepository<CompanyJobDescriptionPoco> repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            logic = new CompanyJobDescriptionLogic(repo);
        }

        [HttpGet]
        [Route("jobdescription")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco),200)]
        public ActionResult GetCompanyJobsDescription(Guid id)
        {
            CompanyJobDescriptionPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("jobdescription")]
        public ActionResult PostCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobdescription")]
        public ActionResult PutCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobdescription")]
        public ActionResult DeleteCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("jobdescription")]
        [ProducesResponseType(typeof(List<CompanyJobDescriptionPoco>), 200)]
        public ActionResult GetAllCompanyJobDescription()
        {
            return Ok(logic.GetAll());
        }
    }
}
