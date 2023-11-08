namespace MallDomain.entity.mall
{
    public class User
    {
        /// <summary>
        /// 用户主键id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; } = null!;

        /// <summary>
        /// 登陆名称(默认为手机号)
        /// </summary>
        public string LoginName { get; set; } = null!;

        /// <summary>
        /// MD5加密后的密码
        /// </summary>
        public string PasswordMd5 { get; set; } = null!;

        /// <summary>
        /// 个性签名
        /// </summary>
        public string IntroduceSign { get; set; } = null!;

        /// <summary>
        /// 注销标识字段(0-正常 1-已注销)
        /// </summary>
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 锁定标识字段(0-未锁定 1-已锁定)
        /// </summary>
        public sbyte LockedFlag { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
