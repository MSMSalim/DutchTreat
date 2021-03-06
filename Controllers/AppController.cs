﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _dutchRepository;

        public AppController(IMailService mailService,
            IDutchRepository dutchRepository)
        {
            this._mailService = mailService;
            this._dutchRepository = dutchRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                // Send the email
                _mailService.SendMail(
                    "shawn@belim.com",
                    model.Subject,
                    $"From: {model.Email}, Message: {model.Message}");

                ViewBag.UserMessage = "Mail Sent!!";

                //clear the form
                ModelState.Clear();
            }
            else
            {
                // show the errors
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        /*[Authorize]*/
        public IActionResult Shop()
        {
            /*

            Before moving to Angular based page

            var results = _dutchRepository.GetAllProducts();

            return View(results);
            */

            return View();
        }
    }
}
