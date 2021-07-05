using AutoMapper;
using EdTech.Api.Controllers;
using EdTech.Api.Entities.Requests;
using EdTech.Api.Entities.Responses;
using EdTech.Core;
using EdTech.Core.Entities;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EdTech.UnitTests
{
    public class AlunosControllerTest
    {
        private readonly AlunosController _alunosController;
        private readonly IAlunosRepository _alunosRepository = A.Fake<IAlunosRepository>();
        private readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper();

        public AlunosControllerTest()
        {
            _alunosController = new AlunosController(_alunosRepository, _mapper);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllAlunos()
        {
            // Arrange
            var qtyAlunos = 3;
            var fakeAlunos = A.CollectionOfDummy<Aluno>(qtyAlunos).ToList();

            A.CallTo(() => _alunosRepository.GetAllAsync()).Returns(Task.FromResult(fakeAlunos));

            // Act
            var actionResult = await _alunosController.GetAll();


            // Assert
            var returnAlunos = actionResult.Value as List<AlunoResponse>;
            Assert.Equal(qtyAlunos, returnAlunos.Count);
        }

        [Fact]
        public async Task GetByRA_ShouldReturnAluno()
        {
            // Arrange
            var aluno = new Aluno
            {
                RA = "RA115115",
                Nome = "João da Silva",
                Email = "joaodasilva@gmail.com",
                Cpf = "346.467.690-00"
            };


            A.CallTo(() => _alunosRepository.GetByRAAsync(aluno.RA)).Returns(Task.FromResult(aluno));


            // Act
            var actionResult = await _alunosController.GetByRA(aluno.RA);


            // Assert
            var returnAluno = actionResult.Value;
            Assert.Equal(aluno.RA, returnAluno.RA);
        }

        [Fact]
        public async Task GetByRA_ShouldReturnBadRequest_WhenAlunoDontExist()
        {
            // Arrange
            var aluno = new Aluno
            {
                RA = "RA115115",
                Nome = "João da Silva",
                Email = "joaodasilva@gmail.com",
                Cpf = "346.467.690-00"
            };


            Aluno fakeAlunoRepositoryResponse = null;
            A.CallTo(() => _alunosRepository.GetByRAAsync(aluno.RA)).Returns(Task.FromResult(fakeAlunoRepositoryResponse));


            // Act
            var actionResult = await _alunosController.GetByRA(aluno.RA);


            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task Create_ShouldReturnAluno()
        {
            // Arrange
            var createAlunoRequest = new CreateAlunoRequest
            {
                RA = "RA115117",
                Nome = "Maria Joaquina",
                Email = "mariajoaquina@gmail.com",
                Cpf = "098.198.920-90"
            };

            var aluno = _mapper.Map<Aluno>(createAlunoRequest);

            Aluno fakeAlunoRepositoryResponse = null;
            A.CallTo(() => _alunosRepository.GetByRAAsync(createAlunoRequest.RA)).Returns(Task.FromResult(fakeAlunoRepositoryResponse));
            A.CallTo(() => _alunosRepository.AddAsync(aluno)).Returns(Task.FromResult(aluno));


            // Act
            var actionResult = await _alunosController.Create(createAlunoRequest);


            // Assert
            var returnAluno = actionResult.Value;
            Assert.Equal(aluno.RA, returnAluno.RA);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenCpfIsInvalid()
        {
            // Arrange
            var createAlunoRequest = new CreateAlunoRequest
            {
                RA = "RA115115",
                Nome = "João da Silva",
                Email = "joaodasilva@gmail.com",
                Cpf = "346.467.690-02"
            };


            // Act
            var actionResult = await _alunosController.Create(createAlunoRequest);


            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenRAExist()
        {
            // Arrange
            var createAlunoRequest = new CreateAlunoRequest
            {
                RA = "RA115115",
                Nome = "João da Silva",
                Email = "joaodasilva@gmail.com",
                Cpf = "346.467.690-02"
            };

            var aluno = _mapper.Map<Aluno>(createAlunoRequest);
            A.CallTo(() => _alunosRepository.GetByRAAsync(createAlunoRequest.RA)).Returns(Task.FromResult(aluno));


            // Act
            var actionResult = await _alunosController.Create(createAlunoRequest);


            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedAluno()
        {
            // Arrange
            var ra = "RA115115";
            var aluno = new Aluno
            {
                RA = "RA115115",
                Nome = "João da Silva",
                Email = "joaodasilva@gmail.com",
                Cpf = "098.198.920-90"
            };

            var updateAlunoRequest = new UpdateAlunoRequest
            {
                RA = "RA115115",
                Nome = "João da Silva Costa",
                Email = "joaodasilvacosta@gmail.com"
            };

            var newAluno = _mapper.Map<Aluno>(updateAlunoRequest);
            newAluno.Cpf = aluno.Cpf;

            A.CallTo(() => _alunosRepository.UpdateAsync(aluno, newAluno)).Returns(Task.FromResult((true, newAluno)));


            // Act
            var actionResult = await _alunosController.Update(ra, updateAlunoRequest);


            // Assert
            Assert.Equal(aluno.RA, newAluno.RA);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenRADontMatch()
        {
            // Arrange
            var ra = "RA115116";
            var aluno = new UpdateAlunoRequest
            {
                RA = "RA115115",
                Nome = "João da Silva",
                Email = "joaodasilva@gmail.com"
            };


            // Act
            var actionResult = await _alunosController.Update(ra, aluno);


            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenAlunoDontExist()
        {
            // Arrange
            var ra = "RA115115";
            var aluno = new UpdateAlunoRequest
            {
                RA = "RA115115",
                Nome = "João da Silva",
                Email = "joaodasilva@gmail.com"
            };

            Aluno fakeAlunoRepositoryResponse = null;
            A.CallTo(() => _alunosRepository.GetByRAAsync(aluno.RA)).Returns(Task.FromResult(fakeAlunoRepositoryResponse));


            // Act
            var actionResult = await _alunosController.Update(ra, aluno);


            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenUpdateFailed()
        {
            // Arrange
            var ra = "RA115115";
            var aluno = new Aluno
            {
                RA = "RA115115",
                Nome = "João da Silva",
                Email = "joaodasilva@gmail.com",
                Cpf = "098.198.920-90"
            };

            var updateAlunoRequest = new UpdateAlunoRequest
            {
                RA = "RA115115",
                Nome = "João da Silva Costa",
                Email = "joaodasilvacosta"
            };

            var newAluno = _mapper.Map<Aluno>(updateAlunoRequest);

            Aluno fakeAlunoRepositoryResponse = null;
            A.CallTo(() => _alunosRepository.UpdateAsync(aluno, newAluno)).Returns(Task.FromResult((false, fakeAlunoRepositoryResponse)));


            // Act
            var actionResult = await _alunosController.Update(ra, updateAlunoRequest);


            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Delete_ShouldDeleteAluno()
        {
            // Arrange
            var fakeAluno = new Aluno
            {
                RA = "RA115117",
                Nome = "Maria Joaquina",
                Email = "mariajoaquina@gmail.com",
                Cpf = "098.198.920-90"
            };

            A.CallTo(() => _alunosRepository.GetByRAAsync(fakeAluno.RA)).Returns(Task.FromResult(fakeAluno));
            A.CallTo(() => _alunosRepository.DeleteAsync(fakeAluno)).Returns(Task.FromResult(fakeAluno));


            // Act
            var actionResult = await _alunosController.Delete(fakeAluno.RA);


            // Assert
            var returnAluno = actionResult.Value;
            Assert.Equal(fakeAluno.RA, returnAluno.RA);
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenAlunoDontExist()
        {
            // Arrange
            var aluno = new Aluno
            {
                RA = "RA115115",
                Nome = "João da Silva",
                Email = "joaodasilva@gmail.com",
                Cpf = "346.467.690-00"
            };

            Aluno fakeAlunoRepositoryResponse = null;
            A.CallTo(() => _alunosRepository.GetByRAAsync(aluno.RA)).Returns(Task.FromResult(fakeAlunoRepositoryResponse));


            // Act
            var actionResult = await _alunosController.Delete(aluno.RA);


            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
