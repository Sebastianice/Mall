namespace MallDomain.entity.common.response {
    public class Result {
        public int Code { get; set; }
        public Object? Data { get; set; }
        public string? Message { get; set; }

    }

    enum ResultCode {
        ERROR = 500,

        SUCCESS = 200,

        UNLOGIN = 416
    }
}
