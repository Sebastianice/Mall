namespace MallDomain.service.mall
{
    public interface IMallUserTokenService
    {

        public Task DeleteMallUserToken(string token);
    }
}
