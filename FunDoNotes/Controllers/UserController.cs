using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagerLayer.Interface;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Enitity;

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
            UserEntity response = userManager.UserLogin(model);

            if (response != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Login Successfull", Data = response });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Login Failed", Data = response });
            }
        }
    }
}
