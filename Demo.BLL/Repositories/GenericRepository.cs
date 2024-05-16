using Demo.BLL.Interfaces;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(T Model)
        {
            if (Model is not null)
                _context.Set<T>().Add(Model);
        }

        public void Delete(T Model)
        {
            if (Model is not null)
                _context.Set<T>().Remove(Model);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee)) 
            {
                return (IEnumerable<T>) await _context.Employees.Include(E => E.Department).ToListAsync();
            }
            else 
            {
                return (IEnumerable<T>) await _context.Departments.Include(E => E.Employees).ToListAsync();
            }


        }

        public async Task<T> GetByIdAsync(int id)
        {
            if(typeof(T) == typeof(Employee))
                return  await _context.Employees.Include(E =>E.Department).FirstOrDefaultAsync(E => E.Id == id) as T;
             else
                return  await _context.Departments.Include(E => E.Employees).FirstOrDefaultAsync(E => E.ID == id) as T;

        }

        public void Update(T Model)
        {
            if (Model is not null)
                _context.Set<T>().Update(Model);
        }
    }
}
