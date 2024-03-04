using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class ColaborationManager : IColaborationManager
    {
        public IColaborationInterface repository;

        public ColaborationManager(IColaborationInterface repository)
        {
            this.repository = repository;
        }
    }
}
