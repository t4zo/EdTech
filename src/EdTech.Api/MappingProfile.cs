using AutoMapper;
using EdTech.Api.Entities.Requests;
using EdTech.Api.Entities.Responses;
using EdTech.Core.Entities;

namespace EdTech
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Aluno, CreateAlunoRequest>().ReverseMap()
                .ForPath(aluno => aluno.Cpf.Codigo,
                        options => options.MapFrom(createAlunoRequest => createAlunoRequest.Cpf));

            CreateMap<Aluno, AlunoResponse>().ReverseMap();
            CreateMap<Aluno, UpdateAlunoRequest>().ReverseMap();
        }
    }
}