using Corelia.DataLake.Dashboard.Apis;
using Corelia.DataLake.Dashboard.Apis.Extentions;
using Corelia.DataLake.Dashboard.Application;
using Corelia.DataLake.Dashboard.Persistance;
using Corelia.DataLake.Dashboard.Shared;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddApis(builder.Configuration);
builder.Services.AddShared(builder.Configuration);
builder.Services.RegesteredPresestantLayer();


builder.Services.AddHttpContextAccessor();


builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
	loggerConfiguration
		.ReadFrom.Configuration(hostingContext.Configuration);
});
builder.Services.AddControllers();
var app = builder.Build();
await app.InitializerEventManagmentContextAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseCors();
app.UseHangfireDashboard("/jobs", new DashboardOptions
{
	Authorization = [
		new HangfireCustomBasicAuthenticationFilter{
			User = app.Configuration.GetValue<string>("HangfireSettings:UserName"),
			Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
		}
		],
	DashboardTitle = "Data lake project Jobs",

}
	);
app.UseStaticFiles();

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();


app.MapHealthChecks(pattern: "health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.Run();
