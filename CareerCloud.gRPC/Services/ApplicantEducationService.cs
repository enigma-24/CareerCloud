using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
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

        public override Task<ApplicantEducationReply> GetApplicantEducation(IdRequest request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = logic.Get(Guid.Parse(request.Id));

            ApplicantEducationReply response = new ApplicantEducationReply
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Major = poco.Major,
                CertificateDiploma = poco.CertificateDiploma,
                StartDate = poco.StartDate == null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate),
                CompletionDate = poco.CompletionDate == null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                CompletionPercent = poco.CompletionPercent == null ? 0 : (byte)poco.CompletionPercent
                // timestamp property not used
            };
            return Task.FromResult(response);
        }
    }
}
