using CommonLayer.ResponseModel;
using ManagerLayer.Interface;
using ManagerLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Enitity;
using RepositoryLayer.Services;

namespace FunDoNotes.Controllers
{
    public class ColabController : ControllerBase
    {
        private readonly IColaborationManager colaborationManager;

        public ColabController(IColaborationManager colaborationManager)
        {
            this.colaborationManager = colaborationManager;
        }

        [Authorize]
        [HttpPost]
        [Route("AddColab")]
        public ActionResult AddUserToColaboration(int UserId, int noteId)
        {
            var response = colaborationManager.AddColaboratory(UserId, noteId);
            if (response != null)
            {
                return Ok(new ResponseModel<ColabEntity>
                {
                    Success = true,
                    Message = "Added Colaborator Successfully",
                    Data = response
                });
            }
            else
            {
                return BadRequest(new ResponseModel<ColabEntity>
                {
                    Success = false,
                    Message = "Failed To add Colaboration",
                    Data = response
                });
            }
        }
        [Authorize]
        [HttpPost]
        [Route("removecolab")]
        public ActionResult RemoveColaboration(int RemoveUserFromNoteId, int UserIdToRemove)
        {
            var response = colaborationManager.RemoveColaborator(RemoveUserFromNoteId, UserIdToRemove);
            if (response != null)
            {
                return Ok(new ResponseModel<ColabEntity>
                {
                    Success = true,
                    Message = "removed user from colaboration",
                    Data = response
                });
            }
            else
            {
                return BadRequest(new ResponseModel<ColabEntity>
                {
                    Success = false,
                    Message = "Failed to remove user from colaboration",
                    Data = response
                });
            }
        }
    }
}
