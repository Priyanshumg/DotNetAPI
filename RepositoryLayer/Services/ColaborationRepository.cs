using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class ColaborationRepository : IColaborationInterface
    {
        private readonly UserContext context;

        public ColaborationRepository(UserContext context)
        {
            this.context = context;
        }
    }
}
