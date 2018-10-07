using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using CVGS.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace CVGS.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var result = SignInStatus.Failure;

                IList<LoginModel> logins = null;

                using (var ctx = new CVGSEntities())
                {
                    logins = ctx.logins.Select(s => new LoginModel()
                    { 
                        user = s.username,
                        pword = s.password
                    }).ToList<LoginModel>();
                }

                foreach (var log in logins)
                {
                    if (Username.Text == log.user && Password.Text == log.pword)
                    {
                        result = SignInStatus.Success;
                        Session["Check"] = true;
                        Session["User"] = Username.Text;
                    }
                }

                    switch (result)
                    {
                        case SignInStatus.Success:
                            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                            break;
                        case SignInStatus.LockedOut:
                            Response.Redirect("/Account/Lockout");
                            break;
                        case SignInStatus.RequiresVerification:
                            Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                            Request.QueryString["ReturnUrl"],
                                                            RememberMe.Checked),
                                              true);
                            break;
                        case SignInStatus.Failure:
                        default:
                            FailureText.Text = "Invalid login attempt";
                            ErrorMessage.Visible = true;
                            break;
                    }
            }
        }
    }
}