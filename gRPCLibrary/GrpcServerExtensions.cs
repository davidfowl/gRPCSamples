using System;
using System.Collections.Generic;
using System.Text;
using Grpc.AspNetCore.Server;
using gRPCLibrary;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GrpcServerExtensions
    {
        public static IServiceCollection AddStandaloneGrpcServer(this IServiceCollection services, 
                                                                Action<KestrelServerOptions> configureServer = null,
                                                                Action<GrpcServiceOptions> configureGrpc = null)
        {
            // Add the standalone GrpcServer (it'll only be added once)
            services.AddHostedService<GrpcServer>();

            services.Configure<GrpcServerOptions>(options =>
            {
                if (configureServer != null)
                {
                    options.ConfigureServerOptions += configureServer;
                }

                if (configureGrpc != null)
                {
                    options.ConfigureGrpcOptions += configureGrpc;
                }
            });
            return services;
        }
    }
}
