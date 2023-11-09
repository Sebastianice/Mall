using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils.validator
{
    internal class IndexConfigUpdateParamValidator : AbstractValidator<IndexConfigUpdateParams>
    {
        //Rules{"ConfigId": {NotEmpty()}, "ConfigName": {NotEmpty()}, "ConfigType": {Ge("1"), Le("5")}, "GoodsId": {NotEmpty()}, "ConfigRank": {Ge("1"), Le("200")}}
        public IndexConfigUpdateParamValidator()
        {
            RuleFor(r => r.ConfigName).NotEmpty().WithMessage("配置名不为空");

            RuleFor(r => r.ConfigType)
                .GreaterThanOrEqualTo((sbyte)1)
                .LessThanOrEqualTo((sbyte)5);

            RuleFor(r => r.GoodsId).NotNull();

            RuleFor(r => r.ConfigRank)
               .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(200);
        }
    }
}
