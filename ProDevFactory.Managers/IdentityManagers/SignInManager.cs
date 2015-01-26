using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProDevFactory.Models.Identity;

namespace ProDevFactory.Managers.IdentityManagers
{
    public class SignInManager : SignInManager<ApplicationUser, string>
    {
        public SignInManager(UserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((UserManager)UserManager);
        }
    }
}
