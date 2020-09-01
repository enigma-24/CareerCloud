using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic logic;
        public SecurityLoginController()
        {
            EFGenericRepository<SecurityLoginPoco> repo = new EFGenericRepository<SecurityLoginPoco>();
            logic = new SecurityLoginLogic(repo);
        }

        [HttpGet]
        [Route("login/{id}")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
        public ActionResult GetSecurityLogin(Guid id)
        {
            SecurityLoginPoco poco = logic.Get(id);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("login")]
        public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("login")]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("login")]
        [ProducesResponseType(typeof(List<SecurityLoginPoco>), 200)]
        public ActionResult GetAllSecurityLogin()
        {
            return Ok(logic.GetAll());
        }
    }
}
