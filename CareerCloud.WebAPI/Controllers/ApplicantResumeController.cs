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
    public class ApplicantResumeController : ControllerBase
    {
        private ApplicantResumeLogic logic;
        public ApplicantResumeController()
        {
            EFGenericRepository<ApplicantResumePoco> repo = new EFGenericRepository<ApplicantResumePoco>();
            logic = new ApplicantResumeLogic(repo);
        }

        [HttpGet]
        [Route("resume/{id}")]
        [ProducesResponseType(typeof(ApplicantResumePoco),200)]
        public IActionResult GetApplicantResume(Guid id)
        {
            ApplicantResumePoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("resume")]
        public IActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("resume")]
        public IActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("resume")]
        public IActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("resume")]
        [ProducesResponseType(typeof(List<ApplicantResumePoco>), 200)]
        public IActionResult GetAllApplicantResume()
        {
            return Ok(logic.GetAll());
        }
    }
}
