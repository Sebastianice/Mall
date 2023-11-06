using FluentValidation;
using MallApi.Controllers.mall;
using MallDomain.entity.mall.request;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils
{
    public static class ValidatorFactory
    {

        public static IValidator<T>? CreateValidator<T>(T obj)
        {
            if (obj is SaveOrderParam)
            {
                return new SaveOrderParamValidator() as IValidator<T>;
            }
            else if (obj is RegisterUserParam)
            {
                return new RegisterUserParamValidator() as IValidator<T>;
            }
            else if (obj is UserLoginParam)
            {
                return new UserLoginParamValidator() as IValidator<T>;
            }
            else if (obj is MallAdminParam)
            {
                return new MallAdminParamValidator() as IValidator<T>;
            }
            else if (obj is MallAdminLoginParam)
            {
                return new MallAdminLoginParamValidator() as IValidator<T>;
            }
            else if (obj is MallUpdateNameParam)
            {
                return new MallUpdateNameParamValidator() as IValidator<T>;
            } else if (obj is MallUpdatePasswordParam)
            {
                return new MallUpdatePasswordParamValidator() as IValidator<T>;
            }
            else
            {
                throw new ArgumentException("Invalid object type");
            }
        }
    }
}
