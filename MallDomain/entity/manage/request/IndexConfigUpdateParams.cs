namespace MallDomain.entity.mannage.request
{

    public class IndexConfigUpdateParams
    {
        public long ConfigId { get; set; }
        public string? ConfigName { get; set; }
        public string? RedirectUrl { get; set; }
        public sbyte ConfigType { get; set; }
        public long GoodsId { get; set; }
        public int ConfigRank { get; set; }
    }


}
