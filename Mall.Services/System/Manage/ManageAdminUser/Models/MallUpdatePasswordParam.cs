using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{

    public class MallUpdatePasswordParam

    {

        public string OriginalPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "新密码不为空")]
        public string NewPassword { get; set; } = null!;
    }

}
