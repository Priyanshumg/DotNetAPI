using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRepository : INotesInterface
    {
        private readonly UserContext context;
        private readonly IConfiguration _config;
        public NotesRepository(UserContext context, IConfiguration _config)
        {
            this.context = context;
            this._config = _config;
        }
    }
}
