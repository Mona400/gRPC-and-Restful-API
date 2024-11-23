using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService_CRUD.Protos;

namespace GrpcService_CRUD.Services
{
    public class AuthenticationService: AuthProtoService.AuthProtoServiceBase
    {
        public override Task<CreateIdentityResponse> GenerateToken(Empty request, ServerCallContext context)
        {
            return base.GenerateToken(request, context);
        }
    }
}
