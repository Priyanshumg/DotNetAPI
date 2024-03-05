using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Enitity;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        { }

        public DbSet<UserEntity> UserTable { get; set; }

        public DbSet<NotesEntity> NotesTable { get; set; }

        public DbSet<LabelEntity> LabelTable { get; set; }

        public DbSet<ColabEntity> ColabTable { get; set; }
    }
}
