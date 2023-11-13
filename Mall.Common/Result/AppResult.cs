namespace Mall.Common.Result
{
    public class AppResult
    {
        public Code ResultCode { get; set; }
        public Object? Data { get; set; }
        public string? Message { get; set; }
        public static AppResult Ok()
        {
            return new AppResult()
            {
                ResultCode = Code.SUCCESS,
                Message = "操作成功"
            };
        }
        public static AppResult OkWithMessage(string message)
        {
            return new AppResult()
            {
                ResultCode = Code.SUCCESS,
                Message = message
            };
        }
        public static AppResult OkWithData(object data)
        {
            return new AppResult()
            {
                ResultCode = Code.SUCCESS,
                Message = "SUCCESS",
                Data = data
            };
        }
        public static AppResult OkWithDetailed(Object data, string message)
        {
            return new AppResult()
            {
                ResultCode = Code.SUCCESS,
                Message = message,
                Data = data
            };
        }
        public static AppResult Fail()
        {
            return new AppResult()
            {
                ResultCode = Code.ERROR,
                Message = "操作失败"
            };
        }
        public static AppResult FailWithMessage(string message)
        {
            return new AppResult()
            {
                ResultCode = Code.ERROR,
                Message = message
            };
        }
        public static AppResult FailWithDetailed(object data, string message)
        {
            return new AppResult()
            {
                ResultCode = Code.ERROR,
                Message = message
            };
        }
        public static AppResult UnLogin(object data)
        {
            return new AppResult()
            {
                ResultCode = Code.UNLOGIN,
                Data = data,
                Message = "未登录"
            };
        }



    }

    public enum Code
    {
        ERROR = 500,

        SUCCESS = 200,

        UNLOGIN = 416
    }
}
