using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils.validator
{
    public class CarouselAddParamValidator : AbstractValidator<CarouselAddParam>
    {
        public CarouselAddParamValidator()
        {
            RuleFor(p => p.CarouselUrl)
                .NotEmpty()
                .WithMessage("轮播图地址不能为空");

            RuleFor(p => p.RedirectUrl)
                .NotEmpty()
                .WithMessage("重定向地址不能为空");

            RuleFor(p => p.CarouselRank)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(200)
                .WithMessage("轮播图地址不能为空");

        }
    }
}
