using System;
using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private SecurityLoginsLogLogic logic;
        public SecurityLoginsLogController()
        {
            EFGenericRepository<SecurityLoginsLogPoco> repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            logic = new SecurityLoginsLogLogic(repo);
        }

        [HttpGet]
        [Route("loginslog/{id}")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        public IActionResult GetSecurityLoginsLog(Guid id)
        {
            SecurityLoginsLogPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("loginslog")]
        public IActionResult PostSecurityLoginsLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("loginslog")]
        public IActionResult PutSecurityLoginsLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("loginslog")]
        public IActionResult DeleteSecurityLoginsLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("loginslog")]
        [ProducesResponseType(typeof(List<SecurityLoginsLogPoco>), 200)]
        public IActionResult GetAllSecurityLoginsLog()
        {
            return Ok(logic.GetAll());
        }
    }
}
