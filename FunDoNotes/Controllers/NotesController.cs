using CommonLayer.RequestModel.NotesModel;
using CommonLayer.ResponseModel;
using ManagerLayer.Interface;
using ManagerLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Enitity;
using System.Collections.Generic;
using System;
using static MassTransit.Monitoring.Performance.BuiltInCounters;
using RepositoryLayer.Entity;
using CommonLayer.RequestModel.LabelModel;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Migrations;
using RepositoryLayer.Interface;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INotesManager noteManger;
        private readonly ILabelManager labelManager;
        public NoteController(INotesManager noteManger,ILabelManager labelManager)
        {
            this.noteManger = noteManger;
            this.labelManager = labelManager;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult AddNote(CreateNotes model)
        {
            int UserId = Convert.ToInt32(User.FindFirst("UserId").Value);
            var response = noteManger.CreateNote(model, UserId);
            if (response != null)
            {
                return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Created Note Success", Data = response });

            }
            else
            {
                return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Create Note Failed", Data = response });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id}", Name = "GetAllNote")]
        public ActionResult FetchData(int UserId)
        {
            List<NotesEntity> data = noteManger.GetAllNote(UserId);
            if (data != null)
            {

                return Ok(new ResponseModel<List<NotesEntity>> { Success = true, Message = "Get Note Successful", Data = data });

            }
            else
            {
                return BadRequest(new ResponseModel<List<NotesEntity>> { Success = false, Message = "Get Note Failure", Data = null });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateNote(int NotesId, UpdateNotesModel model)
        {

            var response = noteManger.UpdateNote(NotesId, model);
            if (response != null)
            {

                return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Update Note Success", Data = response });

            }
            else
            {
                return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Update Note Failed", Data = response });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public ActionResult Trash(int NotesId)
        {

            var response = noteManger.Trash(NotesId);
            if (response != null)
            {

                return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Trash Note Success", Data = response });

            }
            else
            {
                return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Trash Note Failed", Data = response });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteNote(int NotesId)
        {
            try
            {
                int id = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = noteManger.DeleteNoteOperation(NotesId);
                if (response != null)
                {
                    return Ok
                        (new ResponseModel<NotesEntity>
                            { 
                            Success = true, 
                            Message = "Deleted Note", 
                            Data = response 
                            }
                        );
                }
                else
                {
                    return BadRequest
                        (new ResponseModel<NotesEntity> 
                            { 
                                Success = false, 
                                Message = "Failed Deleting Note", 
                                Data = response 
                            }
                        );
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public ActionResult IsArchive(int NotesId)
        {
            try
            {
                var response = noteManger.Archive(NotesId);
                if (response != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "IsArchive Note Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "IsArchive Note Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = ex.Message, Data = null });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Colour")]
        public ActionResult Colour(CreateNotes model, int NotesId)
        {

            var response = noteManger.Colour(model, NotesId);
            if (response != null)
            {
                return Ok(new ResponseModel<NotesEntity>
                {
                    Success = true,
                    Message = "Colour added successfullt",
                    Data = response
                });
            }
            else
            {
                return BadRequest(new ResponseModel<NotesEntity>
                {
                    Success = false,
                    Message = "Failed adding colour",
                    Data = response
                });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Remind")]
        public ActionResult Remind(CreateNotes model, int NotesId)
        {
            var response = noteManger.Reminder(model, NotesId);
            if (response != null)
            {
                return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Reminder Note Success", Data = response });
            }
            else
            {
                return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Reminder Note Failed", Data = response });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UploadImage")]
        public ActionResult UploadImage(string filepath, int NotesId, int Id)
        {
            var response = noteManger.UploadImage(filepath, NotesId, Id);
            if (response != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Upload Image Success", Data = response });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Upload Image Failed", Data = response });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AddLabel")]
        public ActionResult CreateLabel(LabelModel model)
        {
            int noteId = labelManager.GetNoteIdByName(model.LabelName);
            if (noteId == 0)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = $"Note with name '{model.LabelName}' not found", Data = null });
            }

            var createdLabel = labelManager.CreateLabel(model, noteId);
            if (createdLabel != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label created successfully", Data = createdLabel });
            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Failed to create label", Data = null });
            }
        }
        [Authorize]
        [HttpGet("DisplayAllLabels")]
        public IActionResult DisplayAllLabels()
        {
            var response = labelManager.DisplayAllLabel();
            if (response != null)
            {
                var responseData = new ResponseModel<List<LabelEntity>>
                {
                    Success = true,
                    Message = "Displayed all Label successfully",
                    Data = response
                };
                return Ok(responseData);
            }
            else
            {
                return NotFound("No label found based on the provided criteria.");
            }
        }
        [HttpPut]
        [Route("Assign/{labelId}/{noteId}")]
        public ActionResult AssignLabel(int labelId, int noteId)
        {
            var assignedLabel = labelManager.AssignLabel(labelId, noteId);
            if (assignedLabel != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label assigned successfully", Data = assignedLabel });
            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Failed to assign label", Data = null });
            }
        }
        [HttpDelete]
        [Route("Delete/{labelId}")]
        public ActionResult DeleteLabel(int labelId)
        {
            var deletedLabel = labelManager.DeleteLabel(labelId);
            if (deletedLabel != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label deleted successfully", Data = deletedLabel });
            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Failed to delete label", Data = null });
            }
        }
        [HttpPut]
        [Route("Update/{labelId}")]
        public ActionResult UpdateLabel(int labelId, LabelModel model)
        {
            var updatedLabel = labelManager.UpdateLabel(labelId, model);
            if (updatedLabel != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label updated successfully", Data = updatedLabel });
            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Failed to update label", Data = null });
            }
        }


    }
}
