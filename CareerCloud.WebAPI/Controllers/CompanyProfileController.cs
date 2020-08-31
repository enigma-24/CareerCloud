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
    public class CompanyProfileController : ControllerBase
    {
        private CompanyProfileLogic logic;
        public CompanyProfileController()
        {
            EFGenericRepository<CompanyProfilePoco> repo = new EFGenericRepository<CompanyProfilePoco>();
            logic = new CompanyProfileLogic(repo);
        }

        [HttpGet]
        [Route("profile/{id}")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        public IActionResult GetCompanyProfile(Guid id)
        {
            ApplicantEducationPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("profile")]
        public IActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("profile")]
        public IActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("profile")]
        public IActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(typeof(List<CompanyProfilePoco>), 200)]
        public IActionResult GetAllCompanyProfile()
        {
            return Ok(logic.GetAll());
        }
    }
}
