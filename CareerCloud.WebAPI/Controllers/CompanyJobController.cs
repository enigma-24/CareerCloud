﻿using CareerCloud.BusinessLogicLayer;
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
        private readonly CompanyJobLogic logic;
        public CompanyJobController()
        {
            EFGenericRepository<CompanyJobPoco> repo = new EFGenericRepository<CompanyJobPoco>();
            logic = new CompanyJobLogic(repo);
        }

        [HttpGet]
        [Route("job")]
        [ProducesResponseType(typeof(CompanyJobPoco),200)]
        public ActionResult GetCompanyJob(Guid id)
        {
            CompanyJobPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("job")]
        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("job")]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("job")]
        public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("jobs")]
        [ProducesResponseType(typeof(List<CompanyJobPoco>), 200)]
        public ActionResult GetAllCompanyJob()
        {
            return Ok(logic.GetAll());
        }
    }
}
