﻿using CommonLayer.RequestModel.NotesModel;
using CommonLayer.ResponseModel;
using ManagerLayer.Interface;
using ManagerLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Enitity;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System;

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
    }
}
