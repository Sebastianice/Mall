using MallDomain.entity.common.request;

namespace MallDomain.entity.mannage.request
{
    public class MallIndexConfigSearch
    {
        public PageInfo? PageInfo { get; set; }
        public MallIndexConfig? MallIndexConfig { get; set; }
    }

    public class MallIndexConfigAddParams
    {
        public string? ConfigName { get; set; }
        public int ConfigType { get; set; }
        public string? GoodsId { get; set; }
        public string? RedirectUrl { get; set; }
        public string? ConfigRank { get; set; }
    }

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
