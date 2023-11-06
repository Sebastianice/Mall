namespace MallDomain.entity.mannage.request
{
    public class MallAdminLoginParam
    {
        public string? UserName { get; set; }

        public string? PasswordMd5 { get; set; }
    }

    public class MallAdminParam
    {
        public string? LoginUserName { get; set; }

        public string? LoginPassword { get; set; }

        public string NickName { get; set; }
    }

    public class MallUpdateNameParam
    {
        public string? LoginUserName { get; set; }

        public string? NickName { get; set; }
    }

    public class MallUpdatePasswordParam
    {
        public string? OriginalPassword { get; set; }

        public string? NewPassword { get; set; }
    }

}
