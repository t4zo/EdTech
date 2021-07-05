using AutoMapper;
using EdTech.Api.Entities.Requests;
using EdTech.Api.Entities.Responses;
using EdTech.Core;
using EdTech.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdTech.Api.Controllers
{
    public class AlunosController : ControllerBaseApi
    {
        private readonly IAlunosRepository _alunosRepository;
        private readonly IMapper _mapper;

        public AlunosController(IAlunosRepository alunosRespository, IMapper mapper)
        {
            _alunosRepository = alunosRespository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IList<AlunoResponse>>> GetAll()
        {
            var alunos = await _alunosRepository.GetAllAsync();
            return _mapper.Map<List<AlunoResponse>>(alunos);
        }

        [HttpGet("{ra}")]
        public async Task<ActionResult<AlunoResponse>> GetByRA(string ra)
        {
            var aluno = await _alunosRepository.GetByRAAsync(ra);
            if (aluno is null)
            {
                return NotFound();
            }

            return _mapper.Map<AlunoResponse>(aluno);
        }

        [HttpPost]
        public async Task<ActionResult<AlunoResponse>> Create(CreateAlunoRequest createAlunoRequest)
        {
            createAlunoRequest.NormalizeCpf();
            var alunoMapped = _mapper.Map<Aluno>(createAlunoRequest);
            if (!alunoMapped.Cpf.IsValid())
            {
                return BadRequest("Cpf inválido");
            }

            var aluno = await _alunosRepository.GetByRAAsync(alunoMapped.RA);
            if (aluno is not null)
            {
                return BadRequest("RA já cadastrada");
            }

            await _alunosRepository.AddAsync(alunoMapped);

            return _mapper.Map<AlunoResponse>(alunoMapped);
        }

        [HttpPut("{ra}")]
        public async Task<ActionResult<AlunoResponse>> Update(string ra, UpdateAlunoRequest updateAlunoRequest)
        {
            if (!ra.Equals(updateAlunoRequest.RA))
            {
                return BadRequest("RA(s) inválido(s)");
            }

            var aluno = await _alunosRepository.GetByRAAsync(ra);
            if (aluno is null)
            {
                return NotFound();
            }

            var newAluno = _mapper.Map<Aluno>(updateAlunoRequest);
            var (updateSucceeded, updatedAluno) = await _alunosRepository.UpdateAsync(aluno, newAluno);
            if (!updateSucceeded)
            {
                return BadRequest("Dado(s) inválido(s)");
            }

            return _mapper.Map<AlunoResponse>(updatedAluno);
        }

        [HttpDelete("{ra}")]
        public async Task<ActionResult<AlunoResponse>> Delete(string ra)
        {
            var aluno = await _alunosRepository.GetByRAAsync(ra);
            if (aluno is null)
            {
                return NotFound();
            }

            await _alunosRepository.DeleteAsync(aluno);

            return _mapper.Map<AlunoResponse>(aluno);
        }
    }
}
