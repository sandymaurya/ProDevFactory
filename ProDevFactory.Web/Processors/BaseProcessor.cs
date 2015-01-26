using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using ProDevFactory.Managers.Contracts;
using ProDevFactory.Managers.IdentityManagers;

namespace ProDevFactory.Web.Processors
{
    public class BaseProcessor : IDisposable
    {
        public IStudentManager StudentManager { get; set; }

        public UserManager UserManager { get; set; }
        public SignInManager SignInManager { get; set; }
        public IAuthenticationManager AuthenticationManager { get; set; }

        public HttpRequestBase Request { get; set; }
        public HttpSessionStateBase Session { get; set; }
        public ModelStateDictionary ModelState { get; set; }
        public UrlHelper Url { get; set; }
        public dynamic ViewBag { get; set; }

        public void Dispose()
        {
            if (this != null)
                GC.SuppressFinalize(this);
        }
    }
}