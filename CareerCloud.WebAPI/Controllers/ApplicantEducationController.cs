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
    public class ApplicantEducationController : ControllerBase
    {
        private ApplicantEducationLogic logic;
        public ApplicantEducationController()
        {
            EFGenericRepository<ApplicantEducationPoco> repo = new EFGenericRepository<ApplicantEducationPoco>();
            logic = new ApplicantEducationLogic(repo);
        }

        [HttpGet]
        [Route("education/{id}")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        public IActionResult GetApplicantEducation(Guid id)
        {
            ApplicantEducationPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("education")]
        public IActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("education")]
        public IActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("education")]
        public IActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("education")]
        [ProducesResponseType(typeof(List<ApplicantEducationPoco>), 200)]
        public IActionResult GetAllApplicantEducation()
        {
            return Ok(logic.GetAll());
        }
    }
}
