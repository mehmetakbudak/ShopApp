﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System.Net;

namespace ShopApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromBody] LoginModel model)
        {
            var result = _userService.Login(model);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var user = (User)result.Data;
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", $"{user.Name} {user.Surname}");
            }
            return StatusCode((int)result.StatusCode, result);
        }
    }
}