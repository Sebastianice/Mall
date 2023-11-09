namespace MallDomain.entity.mannage.request
{
    public class IndexConfigAddParams
    {
        public string? ConfigName { get; set; }
        public sbyte ConfigType { get; set; }
        public long GoodsId { get; set; }
        public string? RedirectUrl { get; set; }
        public int ConfigRank { get; set; }
    }


}
