using Discount.Grpc.Extensions;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);
var host = Host.CreateDefaultBuilder(args).Build();
host.MigrateDatabase<Program>();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddGrpc();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
});
app.Run();
