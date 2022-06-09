using System;
using System.Threading.Tasks;
using IsikUn.IncubationCentre.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Settings;
using Volo.Abp.Localization;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Settings;
using Volo.Abp.Users;


namespace IsikUn.IncubationCentre.Applications
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IProfileAppService), typeof(ProfileAppService), typeof(MyProfileAppService))]
    public class MyProfileAppService : ProfileAppService
    {
        private readonly IIdentityUserRepository _userRepository;

        public MyProfileAppService(
            IIdentityUserRepository userRepository,
            IdentityUserManager userManager,
            IOptions<IdentityOptions> identityOptions) :
            base(userManager, identityOptions)
        {
            this._userRepository = userRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        public async override Task ChangePasswordAsync(ChangePasswordInput input)
        {
            try
            {
                await base.ChangePasswordAsync(input);
                var currentUser = await UserManager.GetByIdAsync(CurrentUser.GetId());
                currentUser.ExtraProperties.Add("LastPasswordChangeTime", DateTime.Now);
                await _userRepository.UpdateAsync(currentUser);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(L[ex.Message].Value);
            }

        }

    }
}
