using FluentValidation;
using MallDomain.entity.mall.request;

namespace MallDomain.utils.validator
{
    internal class UserLoginParamValidator : AbstractValidator<UserLoginParam>
    {

        public UserLoginParamValidator()
        {
            RuleFor(r => r.LoginName).
                NotEmpty().
                WithMessage("登录名不为空;");

            RuleFor(r => r.PasswordMd5).
                NotEmpty().
                WithMessage("密码不为空;");
        }
    }
}
