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
using static CareerCloud.gRPC.Protos.SystemLanguageCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic logic;

        public SystemLanguageCodeService()
        {
            logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
        }

        public override Task<SystemLanguageCodeReply> GetSystemLanguageCode(SystemLangCodeIdRequest request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = logic.Get(request.LanguageID);
            return Task.FromResult(FromPoco(poco));
        }

        public override Task<SystemLanguageCodeList> GetAllSystemLanguageCode(Empty request, ServerCallContext context)
        {
            SystemLanguageCodeList list = new SystemLanguageCodeList();
            List<SystemLanguageCodePoco> pocos = logic.GetAll();
            foreach (var poco in pocos)
            {
                list.SystemLangCodes.Add(FromPoco(poco));
            }
            return Task.FromResult(list);
        }

        public override Task<Empty> AddSystemLanguageCode(SystemLanguageCodeList request, ServerCallContext context)
        {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();
            foreach (var item in request.SystemLangCodes)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodeList request, ServerCallContext context)
        {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();
            foreach (var item in request.SystemLangCodes)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodeList request, ServerCallContext context)
        {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();
            foreach (var item in request.SystemLangCodes)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private SystemLanguageCodeReply FromPoco(SystemLanguageCodePoco poco)
        {
            return new SystemLanguageCodeReply
            {
                LanguageID = poco.LanguageID,
                Name = poco.Name,
                NativeName = poco.NativeName
            };
        }

        private SystemLanguageCodePoco ToPoco(SystemLanguageCodeReply reply)
        {
            return new SystemLanguageCodePoco
            {
                LanguageID = reply.LanguageID,
                Name = reply.Name,
                NativeName = reply.NativeName
            };
        }
    }
}
