using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils.validator
{
    internal class GoodsInfoUpdateParamValidator : AbstractValidator<GoodsInfoUpdateParam>
    {
        public GoodsInfoUpdateParamValidator()
        {
            RuleFor(r => r.GoodsName)
                .Length(50)
                .WithMessage("商品名不能超过50字符");

            RuleFor(r => r.GoodsIntro)
                .Length(200)
                .WithMessage("商品描述不能超200字符");

            RuleFor(r => r.GoodsId).GreaterThan(1);
            RuleFor(r => r.GoodsCoverImg).NotEmpty();
            RuleFor(r => r.OriginalPrice)
                .GreaterThan(1)
                .LessThan(1000000);
        }
    }
}
