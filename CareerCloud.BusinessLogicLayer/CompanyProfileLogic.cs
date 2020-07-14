using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository) { }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (CompanyProfilePoco poco in pocos)
            {
                if (poco.CompanyWebsite != null && !(poco.CompanyWebsite.EndsWith(".ca") || poco.CompanyWebsite.EndsWith(".com") || poco.CompanyWebsite.EndsWith(".biz")))
                    exceptions.Add(new ValidationException(600, "Valid websites must end with the extensions - '.ca','.com', '.biz'"));

                if(string.IsNullOrWhiteSpace(poco.ContactPhone))
                    exceptions.Add(new ValidationException(601, "Must correspond to a valid phone number"));
                else if(!VerifyPhoneNumber(poco.ContactPhone))
                    exceptions.Add(new ValidationException(601, "Must correspond to a valid phone number"));
            }

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);

            bool VerifyPhoneNumber(string phoneNum)
            {
                string[] phoneNumComponents = phoneNum.Split('-');
                if (phoneNumComponents.Length != 3)
                {
                    return false;
                }
                else
                {
                    if (phoneNumComponents[0].Length != 3)
                        return false;
                    else if (phoneNumComponents[1].Length != 3)
                        return false;
                    else if (phoneNumComponents[2].Length != 4)
                        return false;
                }
                return true;
            }
        }
    }
}
