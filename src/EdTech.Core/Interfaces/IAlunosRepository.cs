using EdTech.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdTech.Core
{
    public interface IAlunosRepository
    {
        Task<List<Aluno>> GetAllAsync();
        Task<Aluno> GetByRAAsync(string ra);
        Task<Aluno> AddAsync(Aluno createAlunoRequest);
        Task<(bool, Aluno)> UpdateAsync(Aluno aluno, Aluno newAluno);
        Task<Aluno> DeleteAsync(Aluno aluno);
    }
}
