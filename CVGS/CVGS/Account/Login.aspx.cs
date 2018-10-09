﻿using System;
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
                bool result = checkLoginStatus(Username.Text, Password.Text);//SignInStatus.Failure;
                
                switch (result)
                {
                    case true:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case false:
                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }

        private bool checkLoginStatus(string enteredUser, string enteredPass)
        {
            IList<LoginModel> logins = null;
            bool result = false;

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
                if (enteredUser == log.user && enteredPass == log.pword)
                {
                    result = true;
                    Session["Check"] = true;
                    Session["User"] = enteredUser;
                }
            }
            return result;
        }
    }
}