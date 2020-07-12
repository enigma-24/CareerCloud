﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco> 
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository):base(repository){}
    }
}
