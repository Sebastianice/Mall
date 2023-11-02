
namespace MallDomain.entity.mall.request {
    public class RegisterUserParam {

        public string? LoginName { get; set; }
        public string? Password { get; set; }
    }
    public class UpdateUserInfoParam {

        public string? NickName { get; set; }
        public string? PasswordMd5 { get; set; }
        public string? IntroduceSign { get; set; }
    }
    public class UserLoginParam {

        public string? LoginName { get; set; }
        public string? PasswordMd5 { get; set; }
    }
}
