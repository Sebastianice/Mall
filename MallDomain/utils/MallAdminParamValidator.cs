using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MallDomain.entity.mannage.request;

namespace MallDomain.utils
{
    public class MallAdminParamValidator: AbstractValidator<MallAdminParam>
    {
        public MallAdminParamValidator()
        {
            RuleFor(r => r.LoginUserName).
                NotEmpty().
                WithMessage("用户名不可为空");

            RuleFor(r => r.LoginPassword).
                NotEmpty().
                WithMessage("登录密码不可为空");

            RuleFor(r => r.NickName).
               NotEmpty().
               WithMessage("昵称不为空;");
        }
    }
}
