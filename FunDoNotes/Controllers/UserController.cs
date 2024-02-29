using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagerLayer.Interface;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Enitity;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("Reg")]
        public ActionResult Register (RegisterModel model)
        {
            var response = userManager.UserRegistration(model);
            if (response != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success= true, Message="Register Successfull", Data=response});
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Register Failed", Data = response });
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginModel model)
        {
            string response = userManager.UserLogin(model);

            if (response != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Login Successfull", Data = response });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Login Failed", Data = response });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public ActionResult ResetPassword(ResetPasswordModel reset)
        {
            try
            {
                string Email = User.FindFirst("UserEmail").Value;
                if (userManager.ResetPassword(Email, reset))
                {
                    return Ok(new ResponseModel<bool> { Success= true, Message= "Password Reset", Data= false});
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "Password cannot be reset", Data = true });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
