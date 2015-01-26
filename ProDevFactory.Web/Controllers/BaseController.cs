using System.Web.Mvc;
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

        protected BaseController(
             IStudentManager studentManager = null,
            UserManager userManager = null,
            SignInManager signInManager = null
            )
        {
            StudentManager = studentManager;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #endregion  Manager Declaration & Definition

        //#region UserManager & SignInManager

        //private UserManager _userManager;
        //private SignInManager _signInManager;

        //protected UserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
        //    }
        //    set
        //    {
        //        _userManager = value;
        //    }
        //}
        //protected SignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
        //    }
        //    private set { _signInManager = value; }
        //}

        //#endregion UserManager & SignInManager

        #region Base Processor

        protected virtual T GetProcessor<T>() where T : BaseProcessor, new()
        {
            return new T()
            {
                StudentManager = StudentManager,
                UserManager = UserManager,
                SignInManager = SignInManager,

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