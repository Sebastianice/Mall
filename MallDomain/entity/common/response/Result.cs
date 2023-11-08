namespace MallDomain.entity.common.response
{
    public class Result
    {
        public Code ResultCode { get; set; }
        public Object? Data { get; set; }
        public string? Message { get; set; }
        public static Result Ok()
        {
            return new Result()
            {
                ResultCode = Code.SUCCESS,
                Message = "操作成功"
            };
        }
        public static Result OkWithMessage(string message)
        {
            return new Result()
            {
                ResultCode = Code.SUCCESS,
                Message = message
            };
        }
        public static Result OkWithData(Object data)
        {
            return new Result()
            {
                ResultCode = Code.SUCCESS,
                Message = "SUCCESS",
                Data = data
            };
        }
        public static Result OkWithDetailed(Object data, string message)
        {
            return new Result()
            {
                ResultCode = Code.SUCCESS,
                Message = message,
                Data = data
            };
        }
        public static Result Fail()
        {
            return new Result()
            {
                ResultCode = Code.ERROR,
                Message = "操作失败"
            };
        }
        public static Result FailWithMessage(string message)
        {
            return new Result()
            {
                ResultCode = Code.ERROR,
                Message = message
            };
        }
        public static Result FailWithDetailed(Object data, string message)
        {
            return new Result()
            {
                ResultCode = Code.ERROR,
                Message = message
            };
        }
        public static Result UnLogin(Object data)
        {
            return new Result()
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
