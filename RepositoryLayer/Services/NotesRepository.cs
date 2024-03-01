using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRepository
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
