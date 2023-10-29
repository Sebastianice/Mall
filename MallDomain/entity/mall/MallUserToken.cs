namespace MallDomain.entity.mall {
    public  class MallUserToken {
    public long UserId { get; set; }
        public string? Token {  get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime ExpireTime { get; set; }
    
    }
}
