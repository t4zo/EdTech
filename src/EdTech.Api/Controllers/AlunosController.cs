using EdTech.Api.Entities.Requests;
using EdTech.Core.Entities;
using EdTech.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdTech.Api.Controllers
{
    public class AlunosController : ControllerBaseApi
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Aluno>>> GetAll()
        {
            return await _context.Alunos.AsNoTracking().Include(x => x.Cpf).ToListAsync();
        }

        [HttpGet("{ra}")]
        public async Task<ActionResult<Aluno>> GetByRA(string ra)
        {
            var aluno = await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(x => x.RA.Equals(ra));
            if(aluno is null)
            {
                return NotFound();
            }

            return aluno;
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> Post(Aluno aluno)
        {
            var result = await _context.Alunos.AddAsync(aluno);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        [HttpPut("{ra}")]
        public async Task<ActionResult<Aluno>> Put(string ra, AlunoRequest alunoRequest)
        {
            if(!ra.Equals(alunoRequest.RA))
            {
                return BadRequest("Data invalid");
            }

            var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.RA.Equals(ra));
            if(aluno == null)
            {
                return NotFound();
            }

            aluno.UpdateNome(alunoRequest.Nome);
            var succeeded = aluno.UpdateEmail(alunoRequest.Email);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{ra}")]
        public async Task<ActionResult<Aluno>> Delete(string ra)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.RA.Equals(ra));
            if (aluno is null)
            {
                return NotFound();
            }

            var result = _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            
            return result.Entity;
        }
    }
}
