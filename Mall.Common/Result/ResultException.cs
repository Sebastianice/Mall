namespace Mall.Common.Result
{
    public partial class ResultException : Exception
    {
        public ResultException(AppResult result, Exception? sourceException)
        {
            AppResult = result;
            SourceException = sourceException;
        }

        /// <summary>
        /// 结果信息
        /// </summary>
        public AppResult AppResult { get; private set; }

        /// <summary>
        /// 源异常
        /// </summary>
        public Exception? SourceException { get; private set; }

        public ResultException() : this(new AppResult(), null) { }
        public ResultException(AppResult result) : this(result, null) { }
        public ResultException(Exception exception) : this(new AppResult(), exception) { }
    }

    public partial class ResultException
    {
        public static ResultException OkWithMessage(string message)
        {
            var result = new AppResult()
            {
                ResultCode = Code.SUCCESS,
                Message = message
            };

            return new ResultException(result);
        }


        public static ResultException OkWithData(Object data)
        {
            var result = new AppResult()
            {
                ResultCode = Code.SUCCESS,
                Message = "SUCCESS",
                Data = data
            };
            return new ResultException(result);
        }


        public static ResultException OkWithDetailed(Object data, string message)
        {
            var result = new AppResult()
            {
                ResultCode = Code.SUCCESS,
                Message = message,
                Data = data
            }; return new ResultException(result);
        }


        public static ResultException Fail()
        {
            var result = new AppResult()
            {
                ResultCode = Code.ERROR,
                Message = "操作失败"
            }; return new ResultException(result);
        }


        public static ResultException FailWithMessage(string message)
        {
            var result = new AppResult()
            {
                ResultCode = Code.ERROR,
               
                Message = message
            };
            return new ResultException(result);
        }


        public static ResultException FailWithDetailed(Object data, string message)
        {
            var result = new AppResult()
            {
                ResultCode = Code.ERROR,
                Message = message
            }; 
            
            return new ResultException(result);
        }


        public static ResultException UnLogin(Object data)
        {
            var result = new AppResult()
            {
                ResultCode = Code.UNLOGIN,
                Data = data,
                Message = "未登录"
            };
            
            return new ResultException(result);
        }

    }
}