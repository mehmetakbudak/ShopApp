﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.API.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
