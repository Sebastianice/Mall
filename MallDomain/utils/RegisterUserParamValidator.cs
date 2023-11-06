using FluentValidation;
using MallDomain.entity.mall.request;

namespace MallApi.Controllers.mall
{
    internal class RegisterUserParamValidator : AbstractValidator<RegisterUserParam>
    {
        public RegisterUserParamValidator()
        {
            RuleFor(p => p.LoginName).NotEmpty().WithMessage("登录名不为空;");
            RuleFor(p => p.Password).NotEmpty().WithMessage("密码不为空;");
        }
    }
}
