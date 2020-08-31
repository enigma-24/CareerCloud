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
    public class CompanyJobSkillController : ControllerBase
    {
        private CompanyJobSkillLogic logic;
        public CompanyJobSkillController()
        {
            EFGenericRepository<CompanyJobSkillPoco> repo = new EFGenericRepository<CompanyJobSkillPoco>();
            logic = new CompanyJobSkillLogic(repo);
        }

        [HttpGet]
        [Route("jobskill")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco),200)]
        public IActionResult GetCompanyJobSkill(Guid id)
        {
            CompanyJobSkillPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("jobskill")]
        public IActionResult PostCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobskill")]
        public IActionResult PutCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobskill")]
        public IActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("jobskill")]
        [ProducesResponseType(typeof(List<CompanyJobSkillPoco>), 200)]
        public IActionResult GetAllCompanyJobSkill()
        {
            return Ok(logic.GetAll());
        }
    }
}
