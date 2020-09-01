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
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic logic;
        public SecurityRoleController()
        {
            EFGenericRepository<SecurityRolePoco> repo = new EFGenericRepository<SecurityRolePoco>();
            logic = new SecurityRoleLogic(repo);
        }

        [HttpGet]
        [Route("role/{id}")]
        [ProducesResponseType(typeof(SecurityRolePoco), 200)]
        public ActionResult GetSecurityRole(Guid id)
        {
            SecurityRolePoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("role")]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("role")]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("role")]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("role")]
        [ProducesResponseType(typeof(List<SecurityRolePoco>), 200)]
        public ActionResult GetAllSecurityRole()
        {
            return Ok(logic.GetAll());
        }
    }
}
