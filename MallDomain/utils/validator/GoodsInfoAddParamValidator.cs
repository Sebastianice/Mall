using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils.validator
{
    internal class GoodsInfoAddParamValidator : AbstractValidator<GoodsInfoAddParam>
    {
        public GoodsInfoAddParamValidator()
        {
            RuleFor(r => r.GoodsName).Length(1, 128).WithMessage("商品名长度限制");

            RuleFor(r => r.GoodsIntro).Length(1, 200).WithMessage("商品名长度限制");

            RuleFor(r => r.GoodsCoverImg).NotEmpty();

            RuleFor(r => r.StockNum).LessThan(100000).GreaterThan(1);

            RuleFor(r => r.StockNum).LessThan(100000).GreaterThan(1);
            RuleFor(r => r.Tag).Length(1, 16);
            RuleFor(r => r.GoodsDetailContent).NotEmpty();
        }
    }
}
