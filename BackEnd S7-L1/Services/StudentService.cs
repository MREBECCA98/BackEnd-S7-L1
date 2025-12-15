using BackEnd_S7_L1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_S7_L1.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        //si usa asNoTracking quando si vuole solo leggere i dati senza modificarli
        public async Task<List<Student>> GetAsNoTracking()
        {
            return await _context.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student> GetByIdAsNoTracking(Guid Id)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id);
        }

        public async Task<Student> GetByNameAsNoTracking(string Nome)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Nome == Nome);
        }

    }
}
