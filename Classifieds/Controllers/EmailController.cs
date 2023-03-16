﻿using Classifieds.Core.Email;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Classifieds.Persistence.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Classifieds.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        
        public EmailController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        
        public ActionResult Write(string title, string receiverEmail)
        {            
            var userId = User.GetUserId();
            var user = _userService.Get(userId);
            var email = new Email { Title = title, ReceiverEmail = receiverEmail, SenderEmail = user.Email };
            ViewData["ReturnUrl"] = HttpContext.Request.Headers["Referer"].ToString();

            return View(email);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Write(Email email, string returnUrl)
        {
            try
            {
                await _emailService.SendEmail(email);
                TempData["Message"] = "Wiadomość została wysłana.";
                return Redirect(returnUrl);
            }
            catch
            {
                return View();
            }
        }
    }
}