﻿using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic logic;
        public ApplicantSkillController()
        {
            EFGenericRepository<ApplicantSkillPoco> repo = new EFGenericRepository<ApplicantSkillPoco>();
            logic = new ApplicantSkillLogic(repo);
        }

        [HttpGet]
        [Route("skill/{id}")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]
        public ActionResult GetApplicantSkill(Guid id)
        {
            ApplicantSkillPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("skill")]
        public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("skill")]
        public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("skill")]
        public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("skill")]
        [ProducesResponseType(typeof(List<ApplicantSkillPoco>), 200)]
        public ActionResult GetAllApplicantSkill()
        {
            return Ok(logic.GetAll());
        }
    }
}
