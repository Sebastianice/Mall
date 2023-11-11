using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils.validator
{
    public class GoodsCategoryReqValidator : AbstractValidator<GoodsCategoryReq>
    {
        public GoodsCategoryReqValidator()
        {
            RuleFor(r => r.CategoryName).NotEmpty().WithMessage("分类名不为空");
        }
    }
}
