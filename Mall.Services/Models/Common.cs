namespace Mall.Services.Models
{

    public class GetById
    {

        public double Id { get; set; }

        public uint Uint()
        {
            return Convert.ToUInt32(Id);
        }
    }

    public class IdsReq
    {

        public List<long> Ids { get; set; } = null!;
    }

    public class GetAuthorityId
    {

        public string? AuthorityId { get; set; }
    }



}
