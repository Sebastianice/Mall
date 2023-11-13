

using Mall.Repository.Models;

namespace Mall.Services.Models
{
    public class UserSearch
    {
        public User? MallUser { get; set; }
        public PageInfo? PageInfo { get; set; }
    }
}
