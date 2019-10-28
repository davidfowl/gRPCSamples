using System;
using System.Collections.Generic;
using System.Text;
using Grpc.AspNetCore.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace gRPCLibrary
{
    public class GrpcServerOptions
    {
        public Action<GrpcServiceOptions> ConfigureGrpcOptions { get; set; } = o => { };
        public Action<KestrelServerOptions> ConfigureServerOptions { get; set; } = o => { };
    }
}
