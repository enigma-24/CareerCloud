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
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic logic;
        public ApplicantJobApplicationController()
        {
            EFGenericRepository<ApplicantJobApplicationPoco> repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            logic = new ApplicantJobApplicationLogic(repo);
        }

        [HttpGet]
        [Route("jobapplication/{id}")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco),200)]
        public ActionResult GetApplicantJobApplication(Guid id)
        {
            ApplicantJobApplicationPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("jobapplication")]
        public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobapplication")]
        public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobapplication")]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("jobapplication")]
        [ProducesResponseType(typeof(List<ApplicantJobApplicationPoco>),200)]
        public ActionResult GetAllApplicantJobApplication()
        {
            return Ok(logic.GetAll());
        }
    }
}
