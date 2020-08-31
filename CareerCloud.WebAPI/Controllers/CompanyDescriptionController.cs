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
    public class CompanyDescriptionController : ControllerBase
    {
        private CompanyDescriptionLogic logic;
        public CompanyDescriptionController()
        {
            EFGenericRepository<CompanyDescriptionPoco> repo = new EFGenericRepository<CompanyDescriptionPoco>();
            logic = new CompanyDescriptionLogic(repo);
        }

        [HttpGet]
        [Route("description/{id}")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco),200)]
        public IActionResult GetCompanyDescription(Guid id)
        {
            CompanyDescriptionPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPut]
        [Route("description")]
        public IActionResult PutCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpPost]
        [Route("description")]
        public IActionResult PostCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("description")]
        public IActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("description")]
        [ProducesResponseType(typeof(List<CompanyDescriptionPoco>), 200)]
        public IActionResult GetAllCompanyDescription()
        {
            return Ok(logic.GetAll());
        }
    }
}
