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
using static CareerCloud.gRPC.Protos.CompanyJobEducation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService: CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic logic;

        public CompanyJobEducationService()
        {
            logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
        }

        public override Task<CompanyJobEducationReply> GetCompanyJobEducation(CompanyJobEducationIdRequest request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = logic.Get(Guid.Parse(request.Id));
            return Task.FromResult(FromPoco(poco));
        }

        public override Task<CompanyJobEducationList> GetAllCompanyJobEducation(Empty request, ServerCallContext context)
        {
            CompanyJobEducationList list = new CompanyJobEducationList();
            List<CompanyJobEducationPoco> pocos = logic.GetAll();
            foreach (var poco in pocos)
            {
                list.CompJobEdus.Add(FromPoco(poco));
            }
            return Task.FromResult(list);
        }

        public override Task<Empty> AddCompanyJobEducation(CompanyJobEducationList request, ServerCallContext context)
        {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();
            foreach (var item in request.CompJobEdus)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> UpdateCompanyJobEducation(CompanyJobEducationList request, ServerCallContext context)
        {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();
            foreach (var item in request.CompJobEdus)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> DeleteCompanyJobEducation(CompanyJobEducationList request, ServerCallContext context)
        {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();
            foreach (var item in request.CompJobEdus)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private CompanyJobEducationReply FromPoco(CompanyJobEducationPoco poco)
        {
            return new CompanyJobEducationReply
            {
                Id = poco.Id.ToString(),
                Importance = poco.Importance,
                Job = poco.Job.ToString(),
                Major = poco.Major,
                TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
            };
        }

        private CompanyJobEducationPoco ToPoco(CompanyJobEducationReply reply)
        {
            return new CompanyJobEducationPoco
            {
                Id = Guid.Parse(reply.Id),
                Importance = (short)reply.Importance,
                Job = Guid.Parse(reply.Job),
                Major = reply.Major
            };
        }
    }
}
