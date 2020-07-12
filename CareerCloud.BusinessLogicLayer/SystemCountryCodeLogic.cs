using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository){}

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            foreach (SystemCountryCodePoco poco in pocos)
            {
               //add
            }
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            foreach (SystemCountryCodePoco poco in pocos)
            {
                //update
            }
        }

        private void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (SystemCountryCodePoco poco in pocos)
            {
                
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
