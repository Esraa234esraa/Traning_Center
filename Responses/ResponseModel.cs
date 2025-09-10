namespace TrainingCenterAPI.Responses
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static ResponseModel<T> SuccessResponse(T data, string message = "")
        {
            return new ResponseModel<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ResponseModel<T> FailResponse(string message)
        {
            return new ResponseModel<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }

    // لو عندك نسخة غير generic:
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static ResponseModel SuccessResponse(string message = "")
        {
            return new ResponseModel
            {
                Success = true,
                Message = message
            };
        }

        public static ResponseModel FailResponse(string message)
        {
            return new ResponseModel
            {
                Success = false,
                Message = message
            };
        }
    }
}
