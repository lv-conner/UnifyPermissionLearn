﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UnifyPermission.Attributes;
using UnifyPermission.Controllers.Base;
using UnifyPermission.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnifyPermission.Controllers
{
    public class LoginController : AuthorizationController
    {
        private readonly IAuthenticationService service;
        public LoginController(IAuthenticationService service)
        {
            this.service = service;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Anonymous]
        public IActionResult Login()
        {
            var model = new UserModel() { Name = "tim", Password = "123456" };
            HttpContext.Response.Cookies.Append("Token", JsonConvert.SerializeObject(model));
            return View();
        }
    }
}
