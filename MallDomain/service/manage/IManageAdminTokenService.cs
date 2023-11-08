namespace MallDomain.service.mannage
{
    public interface IManageAdminTokenService
    {
        public Task DeleteMallAdminUserToken(string token);
    }
}
