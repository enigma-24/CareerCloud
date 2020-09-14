using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantEducation;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService: ApplicantEducationBase
    {
        private readonly ApplicantEducationLogic logic;
        public ApplicantEducationService()
        {
            logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
        }

        public override Task<ApplicantEducationReply> GetApplicantEducation(AppEduIdRequest request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = logic.Get(Guid.Parse(request.Id));

            return Task.FromResult(FromPoco(poco));
        }

        public override Task<ApplicantEducationList> GetAllApplicantEducation(Empty request, ServerCallContext context)
        {
            ApplicantEducationList list = new ApplicantEducationList();
            List<ApplicantEducationPoco> pocos = logic.GetAll();
            foreach (var poco in pocos)
            {
                list.AppEdus.Add(FromPoco(poco));
            }
            return Task.FromResult(list);
        }

        public override Task<Empty> AddApplicantEducation(ApplicantEducationList request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (var reply in request.AppEdus)
            {
                pocos.Add(ToPoco(reply));
            }
            logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> UpdateApplicantEducation(ApplicantEducationList request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (var item in request.AppEdus)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> DeleteApplicantEducation(ApplicantEducationList request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (var item in request.AppEdus)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private ApplicantEducationReply FromPoco(ApplicantEducationPoco poco)
        {
            return new ApplicantEducationReply
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Major = poco.Major,
                CertificateDiploma = poco.CertificateDiploma,
                StartDate = poco.StartDate == null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate),
                CompletionDate = poco.CompletionDate == null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                CompletionPercent = poco.CompletionPercent == null ? 0 : (byte)poco.CompletionPercent,
                Timestamp = ByteString.CopyFrom(poco.TimeStamp)
            };
        }

        private ApplicantEducationPoco ToPoco(ApplicantEducationReply reply)
        {
            return new ApplicantEducationPoco
            {
                Id = Guid.Parse(reply.Id),
                Applicant = Guid.Parse(reply.Applicant),
                Major = reply.Major,
                CertificateDiploma = reply.CertificateDiploma,
                StartDate = reply.StartDate.ToDateTime(),
                CompletionDate = reply.CompletionDate.ToDateTime(),
                CompletionPercent = (byte?)reply.CompletionPercent
            };
        }
    }
}
