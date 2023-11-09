using FluentValidation;

using MallDomain.entity.mall.request;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils.validator
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
            else if (obj is AdminParam)
            {
                return new AdminParamValidator() as IValidator<T>;
            }
            else if (obj is AdminLoginParam)
            {
                return new AdminLoginParamValidator() as IValidator<T>;
            }
            else if (obj is UpdateNameParam)
            {
                return new UpdateNameParamValidator() as IValidator<T>;
            }
            else if (obj is MallUpdatePasswordParam)
            {
                return new MallUpdatePasswordParamValidator() as IValidator<T>;
            }
            else if (obj is GoodsCategoryReq)
            {
                return new GoodsCategoryReqValidator() as IValidator<T>;
            }
            else if (obj is GoodsInfoUpdateParam)
            {
                return new GoodsInfoUpdateParamValidator() as IValidator<T>;
            } else if (obj is IndexConfigAddParams)
            {
                return new IndexConfigAddParamsValidator() as IValidator<T>;
            }
            else
            {
                throw new ArgumentException("Invalid object type");
            }
        }
    }
}
