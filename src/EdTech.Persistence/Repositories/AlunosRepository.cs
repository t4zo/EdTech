
using EdTech.Core;
using EdTech.Core.Entities;
using EdTech.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdTech.Persistence.Repositories
{
    public class AlunosRepository : IAlunosRepository
    {
        private readonly ApplicationDbContext _context;

        public AlunosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Aluno>> GetAllAsync()
        {
            return await _context.Alunos.AsNoTracking().Include(x => x.Cpf).ToListAsync();
        }

        public async Task<Aluno> GetByRAAsync(string ra)
        {
            return await _context.Alunos.FirstOrDefaultAsync(x => x.RA.Equals(ra));
        }

        public async Task<Aluno> AddAsync(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);

            await _context.SaveChangesAsync();

            return aluno;
        }

        public async Task<(bool, Aluno)> UpdateAsync(Aluno aluno, Aluno newAluno)
        {
            var updateEmailSucceeded = aluno.UpdateEmail(newAluno.Email);
            if (updateEmailSucceeded)
            {
                aluno.Nome = newAluno.Nome;
                await _context.SaveChangesAsync();
                return (false, null);
            }

            return (updateEmailSucceeded, newAluno);
        }

        public async Task<Aluno> DeleteAsync(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }
    }
}
