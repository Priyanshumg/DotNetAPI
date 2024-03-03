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

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INotesManager noteManger;
        public NoteController(INotesManager noteManger)
        {
            this.noteManger = noteManger;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult AddNote(CreateNotes model)
        {
            int id = Convert.ToInt32(User.FindFirst("User Id").Value);
            var response = noteManger.CreateNote(model, id);
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
        public ActionResult FetchData(int id)
        {
            List<NotesEntity> data = noteManger.GetAllNote(id);
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
        public ActionResult DeleteNote(int NotesId, int id)
        {

            try
            {
                int idd = Convert.ToInt32(User.FindFirst("User Id").Value);
                var response = noteManger.DeleteNoteOperation(NotesId, id);
                if (response != null)
                {

                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Delete Note Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Delete Note Failed", Data = response });
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
        public ActionResult Colour(int NotesId)
        {

            var response = noteManger.Colour(NotesId);
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
    }
}
