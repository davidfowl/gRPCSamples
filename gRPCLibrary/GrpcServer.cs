using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace gRPCLibrary
{
    // This configures a GrpcServer inside of an existing application
    public class GrpcServer : IHostedService
    {
        private IHost _host;
        private readonly ILoggerFactory _loggerFactory;
        private readonly GrpcServerOptions _options;

        public GrpcServer(ILoggerFactory loggerFactory, IOptions<GrpcServerOptions> options)
        {
            _loggerFactory = loggerFactory;
            _options = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _host = new HostBuilder()
                    .ConfigureServices(services =>
                    {
                        // Add the logger factory so that logs are configured by the main host
                        services.AddSingleton(_loggerFactory);
                    })
                    .ConfigureWebHost(webHostBuilder =>
                    {
                        webHostBuilder.UseKestrel(options =>
                        {
                            options.ConfigureEndpointDefaults(defaults =>
                            {
                                // gRPC requires HTTP/2
                                defaults.Protocols = HttpProtocols.Http2;
                            });

                            _options.ConfigureServerOptions(options);
                        });

                        webHostBuilder.ConfigureServices(services =>
                        {
                            services.AddGrpc(options =>
                            {
                                _options.ConfigureGrpcOptions(options);
                            });
                        });

                        webHostBuilder.Configure(app =>
                        {
                            app.UseRouting();
                            app.UseEndpoints(endpoints =>
                            {
                                endpoints.MapGrpcService<GreeterService>();
                            });
                        });
                    })
                    .Build();

            return _host.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _host?.StopAsync(cancellationToken) ?? Task.CompletedTask;
        }
    }
}
