using Mall.Repository;
using Mall.Repository.Models;
using Mall.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Mall.Services
{
    public class ManageUserService
    {
        private readonly MallContext context;

        public ManageUserService(MallContext context)
        {
            this.context = context;
        }

        // GetMallUserInfoList 分页获取商城注册用户列表
        public async Task<(List<User> list, int total)> GetMallUserInfoList(PageInfo search)
        {
            int limit = search.PageSize;
            int offset = limit * (search.PageNumber - 1);


            int total = await context.Users.CountAsync();

            var userlist = await context.Users
                .OrderByDescending(u => u.CreateTime)
                .Skip(offset)
                .Take(limit)
                .AsNoTracking()
                .ToListAsync();

            return (userlist, total);
        }

        // LockUser 修改用户状态
        public async Task LockUser(List<long> ids, sbyte lockStatus)
        {
            //使用ef core批量操作新特性
            await context.Users
                 .Where(u => ids.Contains(u.UserId))
                 .ExecuteUpdateAsync
                 (s => s.SetProperty
                        (p => p.LockedFlag, lockStatus));

        }
    }
}
