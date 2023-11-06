namespace MallDomain.entity.common.response
{
    public class Result
    {
        public ResultCode Code { get; set; }
        public Object? Data { get; set; }
        public string? Message { get; set; }
        public static Result Ok()
        {
            return new Result()
            {
                Code = ResultCode.SUCCESS,
                Message = "操作成功"
            };
        }
        public static Result OkWithMessage(string message)
        {
            return new Result()
            {
                Code = ResultCode.SUCCESS,
                Message = message
            };
        }
        public static Result OkWithData(Object data)
        {
            return new Result()
            {
                Code = ResultCode.SUCCESS,
                Message = "SUCCESS",
                Data = data
            };
        }
        public static Result OkWithDetailed(Object data, string message)
        {
            return new Result()
            {
                Code = ResultCode.SUCCESS,
                Message = message,
                Data = data
            };
        }
        public static Result Fail()
        {
            return new Result()
            {
                Code = ResultCode.ERROR,
                Message = "操作失败"
            };
        }
        public static Result FailWithMessage(string message)
        {
            return new Result()
            {
                Code = ResultCode.ERROR,
                Message = message
            };
        }
        public static Result FailWithDetailed(Object data, string message)
        {
            return new Result()
            {
                Code = ResultCode.ERROR,
                Message = message
            };
        }
        public static Result UnLogin(Object data)
        {
            return new Result()
            {
                Code = ResultCode.UNLOGIN,
                Data = data,
                Message = "未登录"
            };
        }



    }

    public enum ResultCode
    {
        ERROR = 500,

        SUCCESS = 200,

        UNLOGIN = 416
    }
}
