using MediQ.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
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
				problemDetails = new ProblemDetails
				{
					Type = ProblemDetailsTypes.ENTITY_EXCEPTION,
					Title = "NotFoundException",
					Status = StatusCodes.Status404NotFound,
					Detail = ex.Message
				};
			}
			catch (Exception ex)
			{
				problemDetails = new ProblemDetails
				{
					Type = ProblemDetailsTypes.SERVER_EXCEPTION,
					Title = "Status500InternalServerError",
					Status = StatusCodes.Status500InternalServerError,
					Detail = ex.Message
				};
			}
			if (problemDetails != null)
			{
				context.Response.ContentType = "application/problem+json";
				context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

				var json = JsonConvert.SerializeObject(problemDetails, jsonSerializerSettings);
				await context.Response.WriteAsync(json);
			}

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

	public class CustomIdentityErrorDescriber : IdentityErrorDescriber
	{
		public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = $"یک خطای ناشناخته رخ داده است." }; }
		public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = "رکورد جاری پیشتر ویرایش شده‌است و تغییرات شما آن‌را بازنویسی خواهد کرد." }; }
		public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = "کلمه عبور نادرست است." }; }
		public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = "کلمه عبور نامعتبر است." }; }
		public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "این کاربر قبلأ اضافه شده‌است." }; }
		public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = $"نام کاربری '{userName}' نامعتبر است، فقط می تواند حاوی حروف ویا اعداد باشد." }; }
		public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = $"ایمیل '{email}' نامعتبر است." }; }
		public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"این نام کاربری '{userName}' به کاربر دیگری اختصاص یافته است." }; }
		public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = $"ایمیل '{email}' به کاربر دیگری اختصاص یافته است." }; }
		public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = $"نام نقش '{role}' نامعتبر است." }; }
		public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"این نام نقش '{role}' به کاربر دیگری اختصاص یافته است." }; }
		public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "کلمه‌ی عبور کاربر قبلأ تنظیم شده‌است." }; }
		public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "این کاربر فعال است." }; }
		public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"این نقش '{role}' قبلأ به این کاربر اختصاص یافته است." }; }
		public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = $"این نقش '{role}' قبلأ به این کاربر اختصاص نیافته است." }; }
		public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"کلمه عبور باید حداقل {length} کاراکتر باشد." }; }
		public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "کلمه عبور باید حداقل یک کاراکتر غیر از حروف الفبا داشته باشد." }; }
		public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "کلمه عبور باید حداقل یک عدد داشته باشد." }; }
		public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "کلمه عبور باید حداقل یک حروف کوچک داشته باشد." }; }
		public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "کلمه عبور باید حداقل یک حروف بزرگ داشته باشد." }; }
		public override IdentityError RecoveryCodeRedemptionFailed() { return new IdentityError { Code = nameof(RecoveryCodeRedemptionFailed), Description = "بازیابی ناموفق بود." }; }
		public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) { return new IdentityError { Code = nameof(PasswordRequiresUniqueChars), Description = $"کلمه‌ی عبور باید حداقل داراى {0} حرف متفاوت باشد." }; }
	}
}
