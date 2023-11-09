using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils.validator
{

    public class UpdateNameParamValidator : AbstractValidator<UpdateNameParam>
    {
        public UpdateNameParamValidator()
        {
            RuleFor(r => r.LoginUserName).NotEmpty().WithMessage("登录名不为空");
            RuleFor(r => r.NickName).NotEmpty().WithMessage("昵称不为空");
        }
    }
}
