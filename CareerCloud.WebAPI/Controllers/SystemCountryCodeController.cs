using System;
using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic logic;
        public SystemCountryCodeController()
        {
            EFGenericRepository<SystemCountryCodePoco> repo = new EFGenericRepository<SystemCountryCodePoco>();
            logic = new SystemCountryCodeLogic(repo);
        }

        [HttpGet]
        [Route("countrycode/{code}")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        public ActionResult GetSystemCountryCode(string code)
        {
            SystemCountryCodePoco poco = logic.Get(code);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("countrycode")]
        public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("countrycode")]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("countrycode")]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("countrycode")]
        [ProducesResponseType(typeof(List<SystemCountryCodePoco>), 200)]
        public ActionResult GetAllSystemCountryCode()
        {
            return Ok(logic.GetAll());
        }
    }
}
