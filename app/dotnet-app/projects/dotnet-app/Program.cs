using dotnet_app.Services;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

//string constr = @"Data Source=localhost;Initial Catalog=mono-cro;Connect Timeout=60;Persist Security Info=True;User ID=sa;Password=P@ssword;Encrypt=False";
//SqlConnection connection = new SqlConnection(constr);
//connection.Open();

// Configure the HTTP request pipeline.
app.MapGrpcService<MainBusinessService>();
app.MapGet(" / ", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
