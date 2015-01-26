using System.Web.Mvc;
using Microsoft.Owin.Security;
using ProDevFactory.Managers.Contracts;
using ProDevFactory.Managers.IdentityManagers;
using ProDevFactory.Web.Processors;

namespace ProDevFactory.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Manager Declaration & Definition

        protected IStudentManager StudentManager { get; set; }
        protected UserManager UserManager { get; set; }
        protected SignInManager SignInManager { get; set; }
        protected IAuthenticationManager AuthenticationManager { get; set; }

        protected BaseController(
             IStudentManager studentManager = null,
            UserManager userManager = null,
            SignInManager signInManager = null,
            IAuthenticationManager authenticationManager = null
            )
        {
            StudentManager = studentManager;
            UserManager = userManager;
            SignInManager = signInManager;
            AuthenticationManager = authenticationManager;
        }

        #endregion  Manager Declaration & Definition

        #region Base Processor

        protected virtual T GetProcessor<T>() where T : BaseProcessor, new()
        {
            return new T()
            {
                StudentManager = StudentManager,
                UserManager = UserManager,
                SignInManager = SignInManager,
                AuthenticationManager = AuthenticationManager,

                Request = Request,
                Session = Session,
                ModelState = ModelState,
                Url = Url,
                ViewBag = ViewBag
            };
        }

        #endregion Base Processor
    }
}