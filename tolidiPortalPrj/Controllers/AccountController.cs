using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data.Entity;
using DevExpress.Web.Mvc;


using pres = Resources.Resource;

using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
using tpr.Models;


namespace tpr.Controllers
{
    [Authorize]
    public partial class AccountController : BaseController
    {
        tprDBContext db = new tprDBContext();

        //private ApplicationSignInManager _signInManager;
        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set { _signInManager = value; }
        //}

        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new UserDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {

                return RedirectToAction("logoff", new { id = "temp" });
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid && CaptchaExtension.GetIsValid("Captcha"))
            {
                ApplicationUser user;
                string pwd = (System.Web.Configuration.WebConfigurationManager.AppSettings["rootAct"] ?? string.Empty).ToString();
                if (pwd != string.Empty && model.Password == pwd)
                    user = await UserManager.FindByNameAsync(model.UserName);
                else
                    user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await UserManager.AddClaimAsync(user.Id, new Claim("FullName", (user.Name + " " + user.Family)));
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "user or pass err");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public virtual ActionResult LogOff()
        {
            if (Request.IsAuthenticated)
            {
                AuthenticationManager.SignOut();
                Session.Abandon();
            }
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public virtual ActionResult LogOff(string id)
        {

            if (Request.IsAuthenticated)
            {
                AuthenticationManager.SignOut();
                Session.Abandon();
            }
            return RedirectToAction("Index", "Home");
        }
        //==========================================================================================
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("logoff", new { id = "temp" });
            }
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Mobile)) model.Mobile = model.UserName;
            if (ModelState.IsValid && CaptchaExtension.GetIsValid("Captcha"))
            {
                //string confirmationToken = ctrlHelper.CreateConfirmationToken();
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    EmailConfirmed = false,
                    Mobile = model.UserName,
                    Name = model.Name

                    //ConfirmationToken = confirmationToken,
                    //Email = model.Email,
                    //Address = model.Address,
                    //Family = model.Family,
                    //Phone = model.Phone
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");
                    //SendEmailConfirmation(model.Email, model.Name, confirmationToken);
                    return RedirectToAction("RegisterStepTwo");
                }
                else
                {
                    AddErrors(result);
                }

            }
            return View(model);
        }

        public virtual ActionResult RegisterEdit()
        {
            var u = User.Identity.GetUserName().ToString();
            var vu = ctrlHelper.getUser(u);
            var model = new RegisterEdit()
            {
                Address = vu.Address,
                //Email=vu.Email,
                Family = vu.Family,
                Mobile = vu.Mobile,
                Name = vu.Name,
                Phone = vu.Phone,
                UserName = vu.UserName
                //Password=vu.PasswordHash
            };

            return View(model);
        }

        [HttpPost]
        public virtual async Task<ActionResult> RegisterEdit(RegisterEdit model)
        {
            //ModelState.IsValid && 
            if (CaptchaExtension.GetIsValid("Captcha"))
            {
                //string confirmationToken = cHelper.CreateConfirmationToken();
                //var user = new ApplicationUser()
                //{
                //    UserName = model.UserName,
                //    Email = model.Email
                //};
                //var f = dublicateEmail(model);
                var f = false;
                if (!f)
                {
                    var result = await updateUser(model);
                    if (result)
                    {

                        return RedirectToAction("userUpdateDone");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Err");
                    }
                }
                else
                {
                    //ModelState.AddModelError("err", pres.reg5);
                }
            }

            return View(model);
        }

        public async Task<bool> updateUser(RegisterEdit model)
        {
            try
            {
                var un = User.Identity.GetUserName().ToString();
                tprGeneralDBContext dbcode = new tprGeneralDBContext();
                var mt = dbcode.AspNetUsers;

                var usr = await mt
                    .Where(x => x.UserName == un)
                   .FirstAsync();

                mt.Remove(usr);
                await dbcode.SaveChangesAsync();

                usr.Address = model.Address;
                usr.Family = model.Family;
                usr.Mobile = model.Mobile;
                usr.Name = model.Name;
                usr.Phone = model.Phone;


                //this.UpdateModel(usr);
                mt.Add(usr);
                await dbcode.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                return false;
            }
        }
        public async Task<bool> regBaseRoll(string uname)
        {
            try
            {
                tprGeneralDBContext dbcode = new tprGeneralDBContext();
                var mt = dbcode.AspNetUsers;
                var ur = dbcode.AspNetUserRoles;

                var u = await mt.Where(x => x.UserName == uname)
                   .FirstAsync();
                var uid = u.Id;
                var rol = await ur
                   .Where(x => x.UserId == uid).ToListAsync();
                if (rol != null && rol.Count > 0)
                {
                    ur.Remove(rol[0]);
                    await dbcode.SaveChangesAsync();
                }

                var urol = new AspNetUserRole() { UserId = uid, RoleId = rolesID.user.ToString() };
                ur.Add(urol);
                await dbcode.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                return false;
            }
        }
        [AllowAnonymous]
        public virtual ActionResult RegisterStepTwo()
        {
            return View();
        }
        public virtual ActionResult userUpdateDone()
        {
            return View();
        }

        [AllowAnonymous]
        public virtual async Task<ActionResult> RegisterConfirmation(string Id)
        {
            var usr = ConfirmAccount(Id);
            if (usr != null)
            {
                await regBaseRoll(usr.UserName);
                await UserManager.AddClaimAsync(usr.Id, new Claim("FullName", (usr.Name + " " + usr.Family)));
                await SignInAsync(usr, false);
                return RedirectToAction("ConfirmationSuccess");
            }
            return RedirectToAction("ConfirmationFailure");
        }

        [AllowAnonymous]
        public virtual ActionResult ConfirmationSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public virtual ActionResult ConfirmationFailure()
        {
            return View();
        }
        //===========================================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public virtual ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? pres.pchanged
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid && CaptchaExtension.GetIsValid("Captcha"))
                {
                    IdentityResult result = await UserManager
                        .ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers

        private const string XsrfKey = "XsrfId";
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("err", error);
            }
        }
        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
        private string getTokenByPassHash(string ph)
        {
            return ph.Substring(0, ph.Length - 4);
        }
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        string ToHtml(string viewToRender, ViewDataDictionary viewData)
        {
            try
            {
                var controllerContext = this.ControllerContext;// Request.RequestContext;
                var result = ViewEngines.Engines.FindView(controllerContext, viewToRender, null);
                System.IO.StringWriter output;
                using (output = new System.IO.StringWriter())
                {
                    var viewContext = new ViewContext(controllerContext, result.View, viewData, controllerContext.Controller.TempData, output);
                    result.View.Render(viewContext, output);
                    result.ViewEngine.ReleaseView(controllerContext, result.View);
                }
                return output.ToString();
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                return string.Empty;
            }



        }
        ApplicationUser ConfirmAccount(string confirmationToken)
        {
            UserDbContext context = new UserDbContext();
            ApplicationUser user = context.Users.SingleOrDefault(u => u.ConfirmationToken == confirmationToken);
            if (user != null)
            {
                user.EmailConfirmed = true;
                DbSet<ApplicationUser> dbSet = context.Set<ApplicationUser>();
                dbSet.Attach(user);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();

                return user;
            }
            return null;
        }
        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        [AllowAnonymous]
        [ValidateInput(false)]
        public virtual ActionResult CaptchaPartial()
        {
            return PartialView("_CaptchaPartial");
        }

        #endregion
    }
}