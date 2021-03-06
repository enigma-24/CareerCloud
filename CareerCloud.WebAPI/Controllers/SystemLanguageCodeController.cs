﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic logic;
        public SystemLanguageCodeController()
        {
            EFGenericRepository<SystemLanguageCodePoco> repo = new EFGenericRepository<SystemLanguageCodePoco>();
            logic = new SystemLanguageCodeLogic(repo);
        }

        [HttpGet]
        [Route("languagecode/{languageId}")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        public ActionResult GetSystemLanguageCode(string languageId)
        {
            SystemLanguageCodePoco poco = logic.Get(languageId);
            if (poco != null)
                return Ok(poco);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("languagecode")]
        public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
        {
            logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("languagecode")]
        public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
        {
            logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("languagecode")]
        public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
        {
            logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("languagecode")]
        [ProducesResponseType(typeof(List<SystemLanguageCodePoco>), 200)]
        public ActionResult GetAllSystemLanguageCode()
        {
            return Ok(logic.GetAll());
        }
    }
}
