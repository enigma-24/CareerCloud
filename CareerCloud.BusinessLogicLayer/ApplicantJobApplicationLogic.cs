using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic:BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repository) : base(repository) { }
        
    }
}
