namespace MallDomain.entity.mall
{
    public class MallUser
    {
        public long UserId { get; set; }
        public string? NickName { get; set; }
        public string? LoginName { get; set; }
        public string? PasswordMd5 { get; set; }
        public string? IntroduceSign { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
        public DateTime CreateTime { get; set; }


    }
}
