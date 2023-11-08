using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils
{
    public class MallUpdatePasswordParamValidator : AbstractValidator<MallUpdatePasswordParam>
    {
        public MallUpdatePasswordParamValidator()
        {
            RuleFor(r => r.NewPassword).NotEmpty().WithMessage("新密码不为空");
            RuleFor(r => r.OriginalPassword).NotEmpty().WithMessage("旧密码不为空");
        }
    }
}
