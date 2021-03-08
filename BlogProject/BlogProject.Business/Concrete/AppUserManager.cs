using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.Dtos.AppUserDtos;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IGenericDal<AppUser> _genericDal;
        public AppUserManager(IGenericDal<AppUser> genericDal) : base(genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<AppUser> CheckUserAsync(AppUserSignInDto appUserSignInDto)
        {
            return await _genericDal.GetAsync(I => I.UserName == appUserSignInDto.UserName && I.Password == appUserSignInDto.Password);
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await _genericDal.GetAsync(I => I.UserName == userName);
        }
    }
}
