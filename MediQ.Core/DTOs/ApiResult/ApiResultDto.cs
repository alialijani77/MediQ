namespace MediQ.Core.DTOs.ApiResult
{
	public class ApiResultDto<T>
	{
		public bool IsSuccess { get; set; }

		public string Message { get; set; }

		public T Data { get; set; }

		public int ResponseCode { get; set; }


		public static ApiResultDto<T> CreateSuccess<T>(T data, bool IsSuccess = true, string Message = "عملیات با موفقیت انجام شد.")
		{
			return new ApiResultDto<T>() { Data = data, IsSuccess = IsSuccess, Message = Message, ResponseCode = (int)ResultRsCode.Success };
		}

		public static ApiResultDto<T> NotFound<T>(T data, bool IsSuccess = false, string Message = "مورد درخواستی یافت نشد")
		{
			return new ApiResultDto<T>() { Data = data, IsSuccess = IsSuccess, Message = Message, ResponseCode = (int)ResultRsCode.NotFound };
		}

		public static ApiResultDto<T> BadRequest<T>(T data, bool IsSuccess = false, string Message = "اطلاعات وارد شده صحیح نمی باشد.")
		{
			return new ApiResultDto<T>() { Data = data, IsSuccess = IsSuccess, Message = Message, ResponseCode = (int)ResultRsCode.BadRequest };
		}

		public enum ResultRsCode
		{
			Success = 00,
			NotFound = 404,
			BadRequest = 400,
			ServerError = 500
		}
	}
}
