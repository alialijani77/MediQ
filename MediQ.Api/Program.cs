using Asp.Versioning;
using MediQ.Api.Middlewares;
using MediQ.Domain.Entities.UserManagement;
using MediQ.Infra.Data.DataContext;
using MediQ.Infra.Ioc;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlServerContext");

#region RegisterDependencies
DependencyContainer.RegisterDependencies(builder.Services, connectionString);
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.MediQ", Version = "v1" });
	options.SwaggerDoc("v2", new OpenApiInfo { Title = "WebApi.MediQ", Version = "v2" });


	options.DocInclusionPredicate((doc, apiDescription) =>
	{
		if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

		var version = methodInfo.DeclaringType
			.GetCustomAttributes<ApiVersionAttribute>(true)
			.SelectMany(attr => attr.Versions);

		return version.Any(v => $"v{v.ToString()}" == doc);
	});
});
#endregion

#region ApiVersioning
builder.Services.AddApiVersioning(option =>
{
	option.DefaultApiVersion = new ApiVersion(1);
	option.AssumeDefaultVersionWhenUnspecified = true;
	option.ReportApiVersions = true;
	option.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer();
#endregion

#region Identity
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<BaseContext>().AddDefaultTokenProviders();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
	});
}
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
