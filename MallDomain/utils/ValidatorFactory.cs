using FluentValidation;
using MallApi.Controllers.mall;
using MallDomain.entity.mall.request;

namespace MallDomain.utils {
    public static class ValidatorFactory {

        public static IValidator<T>? CreateValidator<T>(T obj) {
            if (obj is SaveOrderParam) {
                return new SaveOrderParamValidator() as IValidator<T>;
            }
            if (obj is RegisterUserParam) {
                return new RegisterUserParamValidator() as IValidator<T>;
            }
            if (obj is UserLoginParam) {
                return new UserLoginParamValidator() as IValidator<T>;
            } else {
                throw new ArgumentException("Invalid object type");
            }
        }
    }
}
