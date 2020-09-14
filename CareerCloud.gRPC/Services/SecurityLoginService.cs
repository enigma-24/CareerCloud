using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityLogin;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService: SecurityLoginBase
    {
        private readonly SecurityLoginLogic logic;

        public SecurityLoginService()
        {
            logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
        }

        public override Task<SecurityLoginReply> GetSecurityLogin(SecurityLoginIdRequest request, ServerCallContext context)
        {
            SecurityLoginPoco poco = logic.Get(Guid.Parse(request.Id));
            return Task.FromResult(FromPoco(poco));
        }

        public override Task<SecurityLoginList> GetAllSecurityLogin(Empty request, ServerCallContext context)
        {
            SecurityLoginList list = new SecurityLoginList();
            List<SecurityLoginPoco> pocos = logic.GetAll();
            foreach (var poco in pocos)
            {
                list.SecurityLogins.Add(FromPoco(poco));
            }
            return Task.FromResult(list);
        }

        public override Task<Empty> AddSecurityLogin(SecurityLoginList request, ServerCallContext context)
        {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();
            foreach (var item in request.SecurityLogins)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> UpdateSecurityLogin(SecurityLoginList request, ServerCallContext context)
        {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();
            foreach (var item in request.SecurityLogins)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> DeleteSecurityLogin(SecurityLoginList request, ServerCallContext context)
        {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();
            foreach (var item in request.SecurityLogins)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private SecurityLoginReply FromPoco(SecurityLoginPoco poco)
        {
            return new SecurityLoginReply
            {
                Id = poco.Id.ToString(),
                EmailAddress = poco.EmailAddress,
                ForceChangePassword = poco.ForceChangePassword,
                AgreementAccepted = poco.AgreementAccepted == null ? null : Timestamp.FromDateTime((DateTime)poco.AgreementAccepted),
                Created = Timestamp.FromDateTime(poco.Created),
                FullName = poco.FullName,
                IsInactive = poco.IsInactive,
                IsLocked = poco.IsLocked,
                Login = poco.Login,
                Password = poco.Password,
                PasswordUpdate = poco.PasswordUpdate == null ? null : Timestamp.FromDateTime((DateTime)poco.PasswordUpdate),
                PhoneNumber = poco.PhoneNumber,
                PrefferredLanguage = poco.PrefferredLanguage,
                TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
            };
        }

        private SecurityLoginPoco ToPoco(SecurityLoginReply reply)
        {
            return new SecurityLoginPoco
            {
                Id = Guid.Parse(reply.Id),
                EmailAddress = reply.EmailAddress,
                FullName = reply.FullName,
                Login = reply.Login,
                ForceChangePassword = reply.ForceChangePassword,
                Password = reply.Password,
                IsInactive = reply.IsInactive,
                IsLocked = reply.IsLocked,
                PhoneNumber = reply.PhoneNumber,
                PrefferredLanguage = reply.PrefferredLanguage,
                Created = reply.Created.ToDateTime(),
                AgreementAccepted = reply.AgreementAccepted == null ? null : (DateTime?)reply.AgreementAccepted.ToDateTime(),
                PasswordUpdate = reply.PasswordUpdate == null ? null : (DateTime?)reply.PasswordUpdate.ToDateTime()
            };
        }
    }
}
