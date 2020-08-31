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
    public class SecurityLoginsRoleController : ControllerBase
    {
        private SecurityLoginsRoleLogic logic;
        public SecurityLoginsRoleController()
        {
            EFGenericRepository<SecurityLoginsRolePoco> repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            logic = new SecurityLoginsRoleLogic(repo);
        }

        [HttpGet]
        [Route("loginsrole/{id}")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco), 200)]
        public IActionResult GetSecurityLoginsRole(Guid id)
        {
            SecurityLoginsRolePoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("loginsrole")]
        public IActionResult PostSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("loginsrole")]
        public IActionResult PutSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("loginsrole")]
        public IActionResult DeleteSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("loginsrole")]
        [ProducesResponseType(typeof(List<SecurityLoginsRolePoco>), 200)]
        public IActionResult GetAllSecurityLoginsRole()
        {
            return Ok(logic.GetAll());
        }
    }
}
