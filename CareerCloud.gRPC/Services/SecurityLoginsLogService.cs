using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService: SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic logic;

        public SecurityLoginsLogService()
        {
            logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
        }

        public override Task<SecurityLoginsLogReply> GetSecurityLoginsLog(SecurityLoginsLogIdRequest request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = logic.Get(Guid.Parse(request.Id));
            return Task.FromResult(FromPoco(poco));
        }

        public override Task<SecurityLoginsLogList> GetAllSecurityLoginsLog(Empty request, ServerCallContext context)
        {
            SecurityLoginsLogList list = new SecurityLoginsLogList();
            List<SecurityLoginsLogPoco> pocos = logic.GetAll();
            foreach (var poco in pocos)
            {
                list.SecLoginLogs.Add(FromPoco(poco));
            }
            return Task.FromResult(list);
        }

        public override Task<Empty> AddSecurityLoginsLog(SecurityLoginsLogList request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();
            foreach (var item in request.SecLoginLogs)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogList request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();
            foreach (var item in request.SecLoginLogs)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogList request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();
            foreach (var item in request.SecLoginLogs)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private SecurityLoginsLogReply FromPoco(SecurityLoginsLogPoco poco)
        {
            return new SecurityLoginsLogReply
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                IsSuccesful = poco.IsSuccesful,
                LogonDate = Timestamp.FromDateTime(poco.LogonDate),
                SourceIP = poco.SourceIP
            };
        }

        private SecurityLoginsLogPoco ToPoco(SecurityLoginsLogReply reply)
        {
            return new SecurityLoginsLogPoco
            {
                Id = Guid.Parse(reply.Id),
                Login = Guid.Parse(reply.Login),
                IsSuccesful = reply.IsSuccesful,
                SourceIP = reply.SourceIP,
                LogonDate = reply.LogonDate.ToDateTime()
            };
        }
    }
}
