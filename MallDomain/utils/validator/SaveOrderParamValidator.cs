using FluentValidation;
using MallDomain.entity.mall.request;

namespace MallDomain.utils.validator
{
    internal class SaveOrderParamValidator : AbstractValidator<SaveOrderParam>
    {

        public SaveOrderParamValidator()
        {
            RuleFor(r => r.AddressId).
                NotEmpty().
                WithMessage("地址id不为空;");

            RuleFor(r => r.CartItemIds).
                NotEmpty().
                WithMessage("条目id不为空;");
        }
    }
}
