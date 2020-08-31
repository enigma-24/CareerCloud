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
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private ApplicantWorkHistoryLogic logic;
        public ApplicantWorkHistoryController()
        {
            EFGenericRepository<ApplicantWorkHistoryPoco> repo = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            logic = new ApplicantWorkHistoryLogic(repo);
        }

        [HttpGet]
        [Route("workhistory/{id}")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco),200)]
        public IActionResult GetApplicantWorkHistory(Guid id)
        {
            ApplicantWorkHistoryPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("workhistory")]
        public IActionResult PostApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("workhistory")]
        public IActionResult PutApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("workhistory")]
        public IActionResult DeleteApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("workhistory")]
        [ProducesResponseType(typeof(List<ApplicantWorkHistoryPoco>), 200)]
        public IActionResult GetAllApplicantWorkHistory()
        {
            return Ok(logic.GetAll());
        }
    }
}
