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
        private readonly SecurityLoginsLogLogic logic;
        public SecurityLoginsLogController()
        {
            EFGenericRepository<SecurityLoginsLogPoco> repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            logic = new SecurityLoginsLogLogic(repo);
        }

        [HttpGet]
        [Route("loginslog/{id}")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        public ActionResult GetSecurityLoginLog(Guid id)
        {
            SecurityLoginsLogPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("loginslog")]
        public ActionResult PostSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("loginslog")]
        public ActionResult PutSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("loginslog")]
        public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("loginslog")]
        [ProducesResponseType(typeof(List<SecurityLoginsLogPoco>), 200)]
        public ActionResult GetAllSecurityLoginsLog()
        {
            return Ok(logic.GetAll());
        }
    }
}
