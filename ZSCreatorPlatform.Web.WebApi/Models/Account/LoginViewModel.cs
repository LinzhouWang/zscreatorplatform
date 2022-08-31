using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.WebApi.Models.Account
{
    public class LoginViewModel
    {

        [Required(ErrorMessage ="账号名不能为空")]
        public string Name { get; set; }


        [Required(ErrorMessage ="密码不能为空")]
        public string Password { get; set; }
    }
}
