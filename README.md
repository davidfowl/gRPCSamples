# gRPC Samples

Samples showing how to run a gRPC server in a WPF application and as a standalone server running in IIS (Kestrel and IIS running on different ports).
WPF has a known issue generating the appropriate gRPC and proto files (see https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0#wpf-projects-unable-to-generate-grpc-c-assets-from-proto-files)
so the gRPC service is in a class library. Code generation works fine in web projects but I wanted to share the code.