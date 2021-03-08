using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.Tools.JWT
{
    public interface IJwtService
    {
        JwtToken GenerateJwt(AppUser appUser);
    }
}
