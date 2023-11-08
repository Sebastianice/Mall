namespace MallDomain.entity.mannage
{
    public class AdminUser
    {

        /// <summary>
        /// 管理员id
        /// </summary>
        public long AdminUserId { get; set; }

        /// <summary>
        /// 管理员登陆名称
        /// </summary>
        public string LoginUserName { get; set; } = null!;

        /// <summary>
        /// 管理员登陆密码
        /// </summary>
        public string LoginPassword { get; set; } = null!;

        /// <summary>
        /// 管理员显示昵称
        /// </summary>
        public string NickName { get; set; } = null!;

        /// <summary>
        /// 是否锁定 0未锁定 1已锁定无法登陆
        /// </summary>
        public sbyte? Locked { get; set; }
    }
}
