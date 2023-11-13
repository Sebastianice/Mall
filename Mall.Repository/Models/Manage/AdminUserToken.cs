namespace Mall.Repository.Models
{
    public class AdminUserToken
    {
        /// <summary>
        /// 用户主键id
        /// </summary>
        public long AdminUserId { get; set; }

        /// <summary>
        /// token值(32位字符串)
        /// </summary>
        public string Token { get; set; } = null!;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// token过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }


    }
}
