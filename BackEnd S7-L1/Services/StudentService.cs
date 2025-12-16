using BackEnd_S7_L1.Models.Dto;
using BackEnd_S7_L1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BackEnd_S7_L1.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        //si usa asNoTracking quando il dato non si deve modificare cosi non occupa memoria

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

        public async Task<List<Student>> SearchByTermAsNoTracking(string searchTerm)
        {
            return await _context.Students
                .AsNoTracking()
               .Where(s =>
                      EF.Functions.Like(s.Nome, $"%{searchTerm}%") ||
                      EF.Functions.Like(s.Cognome, $"%{searchTerm}%")
)
                .ToListAsync();
        }


        public async Task<StudentResponseDto?> Delete(Guid Id)
        {
            Student student = await this.GetById(Id);

            if (student is not null)
            {
                _context.Remove(student);
                await _context.SaveChangesAsync();

                return new StudentResponseDto
                {
                    Id = student.Id,
                    Nome = student.Nome,
                    Cognome = student.Cognome,
                    Email = student.Email
                };
            }

            return null;
        }


        public async Task<Student> GetById(Guid Id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == Id);
        }


        public async Task<bool> Save()
        {
            return await this._context.SaveChangesAsync() > 0;
        }

    }
}
