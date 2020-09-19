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
using static CareerCloud.gRPC.Protos.ApplicantProfile;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService: ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic logic;

        public ApplicantProfileService()
        {
            logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
        }

        public override Task<ApplicantProfileReply> GetApplicantProfile(ApplicantProfileIdRequest request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = logic.Get(Guid.Parse(request.Id));
            return Task.FromResult(FromPoco(poco));
        }

        public override Task<ApplicantProfileList> GetAllApplicantProfile(Empty request, ServerCallContext context)
        {
            ApplicantProfileList list = new ApplicantProfileList();
            List<ApplicantProfilePoco> pocos = logic.GetAll();
            foreach (var poco in pocos)
            {
                list.AppProfiles.Add(FromPoco(poco));
            }
            return Task.FromResult(list);
        }

        public override Task<Empty> AddApplicantProfile(ApplicantProfileList request, ServerCallContext context)
        {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();
            foreach (var item in request.AppProfiles)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateApplicantProfile(ApplicantProfileList request, ServerCallContext context)
        {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();
            foreach (var item in request.AppProfiles)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteApplicantProfile(ApplicantProfileList request, ServerCallContext context)
        {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();
            foreach (var item in request.AppProfiles)
            {
                pocos.Add(ToPoco(item));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private ApplicantProfileReply FromPoco(ApplicantProfilePoco poco)
        {
            return new ApplicantProfileReply
            {
                Id = poco.Id.ToString(),
                City = poco.City,
                Country = poco.Country,
                Currency = poco.Currency,
                Login = poco.Login.ToString(),
                PostalCode = poco.PostalCode,
                Province = poco.Province,
                Street = poco.Street,
                Timestamp = ByteString.CopyFrom(poco.TimeStamp)
            };
        }

        private ApplicantProfilePoco ToPoco(ApplicantProfileReply reply)
        {
            return new ApplicantProfilePoco
            {
                Id = Guid.Parse(reply.Id),
                City = reply.City,
                Country = reply.Country,
                Currency = reply.Country,
                Login = Guid.Parse(reply.Login),
                PostalCode = reply.PostalCode,
                Province = reply.Province,
                Street = reply.Street,
                TimeStamp = reply.Timestamp.ToByteArray()
            };
        }
    }
}
