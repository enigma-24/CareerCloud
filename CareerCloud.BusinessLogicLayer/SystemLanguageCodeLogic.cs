using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository) {}

        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            foreach (SystemLanguageCodePoco poco in pocos)
            {
                //add
            }
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            foreach (SystemLanguageCodePoco poco in pocos)
            {
                //update
            }
        }

        private void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (SystemLanguageCodePoco poco in pocos)
            {

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
