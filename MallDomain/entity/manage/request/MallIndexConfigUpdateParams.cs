namespace MallDomain.entity.mannage.request
{

    public class MallIndexConfigUpdateParams
    {
        public int ConfigId { get; set; }
        public string? ConfigName { get; set; }
        public string? RedirectUrl { get; set; }
        public int ConfigType { get; set; }
        public int GoodsId { get; set; }
        public string? ConfigRank { get; set; }
    }


}
