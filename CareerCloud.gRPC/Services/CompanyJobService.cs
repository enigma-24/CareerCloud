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
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService: CompanyJobBase
    {
        private readonly CompanyJobLogic logic;

        public CompanyJobService()
        {
            logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }

        public override Task<CompanyJobReply> GetCompanyJob(CompanyJobIdRequest request, ServerCallContext context)
        {
            CompanyJobPoco poco = logic.Get(Guid.Parse(request.Id));
            return Task.FromResult(FromPoco(poco));
        }

        public override Task<CompanyJobList> GetAllCompanyJob(Empty request, ServerCallContext context)
        {
            CompanyJobList list = new CompanyJobList();
            List<CompanyJobPoco> pocos = logic.GetAll();
            foreach (var poco in pocos)
            {
                list.CompanyJobs.Add(FromPoco(poco));
            }
            return Task.FromResult(list);
        }

        public override Task<Empty> AddCompanyJob(CompanyJobList request, ServerCallContext context)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();
            foreach (var item in request.CompanyJobs)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> UpdateCompanyJob(CompanyJobList request, ServerCallContext context)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();
            foreach (var item in request.CompanyJobs)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> DeleteCompanyJob(CompanyJobList request, ServerCallContext context)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();
            foreach (var item in request.CompanyJobs)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private CompanyJobReply FromPoco(CompanyJobPoco poco)
        {
            return new CompanyJobReply
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                IsCompanyHidden = poco.IsCompanyHidden,
                IsInactive = poco.IsInactive,
                ProfileCreated = Timestamp.FromDateTime(poco.ProfileCreated),
                TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
            };
        }

        private CompanyJobPoco ToPoco(CompanyJobReply reply)
        {
            return new CompanyJobPoco
            {
                Id = Guid.Parse(reply.Id),
                Company = Guid.Parse(reply.Company),
                ProfileCreated = reply.ProfileCreated.ToDateTime(),
                IsCompanyHidden = reply.IsCompanyHidden,
                IsInactive = reply.IsInactive
            };
        }
    }
}
