using Asp.Versioning;
using MediQ.Api.Middlewares;
using MediQ.Domain.Entities.UserManagement;
using MediQ.Infra.Data.DataContext;
using MediQ.Infra.Ioc;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;
using System.Reflection;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	var connectionString = builder.Configuration.GetConnectionString("SqlServerContext");

	#region Nlog
	builder.Logging.ClearProviders();
	builder.Host.UseNLog();
	#endregion

    #region RegisterDependencies
    DependencyContainer.RegisterDependencies(builder.Services, connectionString);
    builder.Services.AddTransient<RequestTimingMiddleware>(); //ToDo: how to moved this line to IoC
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
	builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<BaseContext>().AddDefaultTokenProviders().AddErrorDescriber<CustomIdentityErrorDescriber>();
	builder.Services.Configure<IdentityOptions>(option =>
	{
		#region user
		//option.User.AllowedUserNameCharacters = "abcd1234";
		option.User.RequireUniqueEmail = true;
		#endregion

		#region Password
		option.Password.RequireDigit = false;
		option.Password.RequireLowercase = false;
		option.Password.RequireUppercase = false;
		option.Password.RequiredLength = 0;
		option.Password.RequireNonAlphanumeric = false; //!@#$%^&*()_+
		option.Password.RequiredUniqueChars = 1;
		#endregion

		

	});
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

    app.UseMiddleware<RequestTimingMiddleware>();

    app.UseCustomExceptionHandler();

	app.UseHttpsRedirection();
	app.UseAuthentication();
	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
catch (Exception ex)
{
	logger.Error(ex);
}
finally
{
	LogManager.Shutdown();
}