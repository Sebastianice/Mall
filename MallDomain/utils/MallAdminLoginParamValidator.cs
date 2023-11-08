using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils
{
    public class MallAdminLoginParamValidator : AbstractValidator<MallAdminLoginParam>
    {
        public MallAdminLoginParamValidator()
        {
            RuleFor(r => r.PasswordMd5).NotEmpty().WithMessage("密码不为空");
            RuleFor(r => r.UserName).NotEmpty().WithMessage("用户名不为空");
        }
    }
}
