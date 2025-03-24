using MediQ.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MediQ.Api.Middlewares
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class CustomExceptionHandler
	{
		private readonly RequestDelegate _next;
		private readonly JsonSerializerSettings jsonSerializerSettings;

		public CustomExceptionHandler(RequestDelegate next)
		{
			_next = next;
			jsonSerializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
		}

		public async Task Invoke(HttpContext context)
		{

			ProblemDetails problemDetails = null;
			try
			{
				await _next(context);
			}
			catch (NotFoundException ex)
			{
				problemDetails = new ProblemDetails { Type = ProblemDetailsTypes.ENTITY_EXCEPTION,  Title = "NotFoundException"
					, Status = StatusCodes.Status404NotFound , Detail = ex.Message};
			}
			catch (Exception ex)
			{
				throw;
			}
			context.Response.ContentType = "application/problem+json";
			context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

			var json = JsonConvert.SerializeObject(problemDetails, jsonSerializerSettings);
			await context.Response.WriteAsync(json);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class CustomExceptionHandlerExtensions
	{
		public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomExceptionHandler>();
		}
	}

	public static class ProblemDetailsTypes
	{
		public const string BASE = "";

		public const string MODEL_VALIDATION = BASE + "ModelValidation";
		public const string INVALID_DELETE = BASE + "InvalidDelete";
		public const string ENTITY_NOT_FOUND = BASE + "EntityNotFound";
		public const string ENTITY_EXCEPTION = BASE + "EntityException";
		public const string UNAUTHORIZED_EXCEPTION = BASE + "UnAuthorizedException";
		public const string SERVER_EXCEPTION = BASE + "ServerException";

		public const string UNHANDLED_EXCEPTION = BASE + "UnhandledException";
		public const string DEVELOPMENT_UNHANDLED_EXCEPTION = BASE + "DevelopmentUnhandledException";
	}
}
