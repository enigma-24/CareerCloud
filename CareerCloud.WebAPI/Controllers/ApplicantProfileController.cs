using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private ApplicantProfileLogic logic;
        public ApplicantProfileController()
        {
            EFGenericRepository<ApplicantProfilePoco> repo = new EFGenericRepository<ApplicantProfilePoco>();
            logic = new ApplicantProfileLogic(repo);
        }

        [HttpGet]
        [Route("profile/{id}")]
        [ProducesResponseType(typeof(ApplicantProfilePoco),200)]
        public IActionResult GetApplicantProfile(Guid id)
        {
            ApplicantProfilePoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("profile")]
        public IActionResult PostApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("profile")]
        public IActionResult PutApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("profile")]
        public IActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(typeof(List<ApplicantProfilePoco>), 200)]
        public IActionResult GetAllApplicantProfile()
        {
            return Ok(logic.GetAll());
        }
    }
}
