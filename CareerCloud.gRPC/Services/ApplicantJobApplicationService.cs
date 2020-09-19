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
using static CareerCloud.gRPC.Protos.ApplicantJobApplication;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService: ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic logic;

        public ApplicantJobApplicationService()
        {
            logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
        }

        public override Task<ApplicantJobApplicationReply> GetApplicantJobApplication(ApplicantJobApplicationIdRequest request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco poco = logic.Get(Guid.Parse(request.Id));
            return Task.FromResult(FromPoco(poco));
        }

        public override Task<ApplicantJobApplicationList> GetAllApplicantJobApplication(Empty request, ServerCallContext context)
        {
            ApplicantJobApplicationList list = new ApplicantJobApplicationList();
            List<ApplicantJobApplicationPoco> pocos = logic.GetAll();
            foreach (var poco in pocos)
            {
                list.AppJobApps.Add(FromPoco(poco));
            }
            return Task.FromResult(list);
        }

        public override Task<Empty> AddApplicantJobApplication(ApplicantJobApplicationList request, ServerCallContext context)
        {
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();
            foreach (var item in request.AppJobApps)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplicationList request, ServerCallContext context)
        {
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();
            foreach (var item in request.AppJobApps)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplicationList request, ServerCallContext context)
        {
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();
            foreach (var item in request.AppJobApps)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private ApplicantJobApplicationReply FromPoco(ApplicantJobApplicationPoco poco)
        {
            return new ApplicantJobApplicationReply
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
                ApplicationDate = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.ApplicationDate,DateTimeKind.Utc)),
                Timestamp = ByteString.CopyFrom(poco.TimeStamp)
            };
        }

        private ApplicantJobApplicationPoco ToPoco(ApplicantJobApplicationReply reply)
        {
            return new ApplicantJobApplicationPoco
            {
                Id = Guid.Parse(reply.Id),
                Applicant = Guid.Parse(reply.Applicant),
                Job = Guid.Parse(reply.Job),
                ApplicationDate = reply.ApplicationDate.ToDateTime(),
                TimeStamp = reply.Timestamp.ToByteArray()
            };
        }
    }
}
