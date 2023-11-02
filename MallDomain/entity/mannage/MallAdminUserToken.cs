namespace MallDomain.entity.mannage {
    public class MallAdminUserToken {

        public long AdminUserId { get; set; }
        public string? Token { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime ExpireTime { get; set; }




    }
}
