using DAL.Interfaces;
using DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UsuarioRepository:Repository<Usuario>,IUsuarioRepository
    {
        public UsuarioRepository(DbContext  dbContext) : base(dbContext) { }
    }
}
