using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityRoleLogic:BaseLogic<SecurityRolePoco>
    {
        public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repository):base(repository){}
    }
}
