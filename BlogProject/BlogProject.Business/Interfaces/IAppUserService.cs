using BlogProject.DTO.Dtos.AppUserDtos;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> CheckUserAsync(AppUserSignInDto appUserSignInDto);
        Task<AppUser> FindByNameAsync(string userName);
    }
}
