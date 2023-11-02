namespace MallDomain.entity.common.request {
    public class PageInfo {

        public int PageNumber { get; set; }


        public int PageSize { get; set; }
    }

    public class GetById {

        public double Id { get; set; }

        public uint Uint() {
            return Convert.ToUInt32(Id);
        }
    }

    public class IdsReq {

        public List<int> Ids { get; set; }
    }

    public class GetAuthorityId {

        public string? AuthorityId { get; set; }
    }



}
