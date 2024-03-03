using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagerLayer.Interface;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Enitity;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Authorization;
using MassTransit;
using System.Threading.Tasks;
using ManagerLayer.Services;
using CommonLayer.Utilities;
using CommonLayer.RequestModel.LoginPageModel;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly IBus bus;
        public UserController(IUserManager userManager, IBus bus)
        {
            this.userManager = userManager;
            this.bus = bus;
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
                    return Ok(new ResponseModel<bool> { 
                        Success= true,
                        Message= "Password Reset", 
                        Data= true
                    }
                    );
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { 
                        Success = false,
                        Message = "Password cannot be reset",
                        Data = true });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(string Email)
        {
            try
            {
                if (Email != null)
                {
                    SendMail sendMail = new SendMail();
                    ForgetPasswordModel token = userManager.ForgetPassword(Email);
                    sendMail.SendEmail(Email, token);
                    Uri uri = new Uri("rabbitmq://localhost/FundooNotesEmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);

                    await endPoint.Send(token);

                    return Ok(new ResponseModel<string> { Success = true, Message = "Mail Sent Successfully", Data = token.token });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Email Doesn't Exist", Data = "else part is executed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = ex.Message, Data = "" });
            }

        }

    }
}
