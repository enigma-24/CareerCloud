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
using static CareerCloud.gRPC.Protos.CompanyDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService: CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic logic;

        public CompanyDescriptionService()
        {
            logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
        }

        public override Task<CompanyDescriptionReply> GetCompanyDescription(CompanyDescriptionIdRequest request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = logic.Get(Guid.Parse(request.Id));
            return Task.FromResult(FromPoco(poco));
        }

        public override Task<CompanyDescriptionList> GetAllCompanyDescription(Empty request, ServerCallContext context)
        {
            CompanyDescriptionList list = new CompanyDescriptionList();
            List<CompanyDescriptionPoco> pocos = logic.GetAll();
            foreach (var poco in pocos)
            {
                list.CompDescList.Add(FromPoco(poco));
            }
            return Task.FromResult(list);
        }

        public override Task<Empty> AddCompanyDescription(CompanyDescriptionList request, ServerCallContext context)
        {
            List<CompanyDescriptionPoco> pocos = new List<CompanyDescriptionPoco>();
            foreach (var item in request.CompDescList)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptionList request, ServerCallContext context)
        {
            List<CompanyDescriptionPoco> pocos = new List<CompanyDescriptionPoco>();
            foreach (var item in request.CompDescList)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptionList request, ServerCallContext context)
        {
            List<CompanyDescriptionPoco> pocos = new List<CompanyDescriptionPoco>();
            foreach (var item in request.CompDescList)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private CompanyDescriptionReply FromPoco(CompanyDescriptionPoco poco)
        {
            return new CompanyDescriptionReply
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                CompanyDescription = poco.CompanyDescription,
                CompanyName = poco.CompanyName,
                LanguageId = poco.LanguageId,
                TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
            };
        }

        private CompanyDescriptionPoco ToPoco(CompanyDescriptionReply reply)
        {
            return new CompanyDescriptionPoco
            {
                Id = Guid.Parse(reply.Id),
                Company = Guid.Parse(reply.Company),
                CompanyDescription = reply.CompanyDescription,
                CompanyName = reply.CompanyName,
                LanguageId = reply.LanguageId,
                TimeStamp = reply.TimeStamp.ToByteArray()
            };
        }
    }
}
