using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils
{
    
    public class MallUpdateNameParamValidator : AbstractValidator<MallUpdateNameParam>
    {
        public MallUpdateNameParamValidator()
        {
            RuleFor(r => r.LoginUserName).NotEmpty().WithMessage("登录名不为空");
            RuleFor(r => r.NickName).NotEmpty().WithMessage("昵称不为空");
        }
    }
}
