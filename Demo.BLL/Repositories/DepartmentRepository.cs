using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Department>> GetByNameAsync(string name)
        {
            return  await _context.Departments.Where(D =>D.Name.ToLower().Contains(name.ToLower())).Include(D => D.Employees).ToListAsync();
        }
    }
}
