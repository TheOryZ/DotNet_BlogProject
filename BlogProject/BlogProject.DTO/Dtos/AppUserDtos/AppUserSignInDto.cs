using BlogProject.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DTO.Dtos.AppUserDtos
{
    public class AppUserSignInDto : IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
