# EdTech - Grupo A
Projeto Backend do desafio [Fullstack](https://github.com/grupo-a/orbita-challenge-full-stack-web). Para acessar o projeto Frontend, [Clique aqui](https://github.com/t4zo/edtech-frontend)

## Importante
Para integração com o frontend, é necessário que o este projeto rode no endereço: https://localhost:5001

# Decisões Arquiteturais
Foram utilizados 4 projetos na solução afim de obter um menor acomplamento e maior coesão entre partes individuais, sendo eles:
- **EdTech.Core**: Responsável pelas regras de negócio da aplicação e componentes centrais comuns entre outros eventuais projetos;
- **EdTech.Persistence**: Responsável pela camada de configuração e manipulação do banco de dados;
- **EdTech.Api**: Interface responsável pelas requisições recebidas, manipulação dos dados e resposta às requisições;
- **EdTech.UnitTests**: Teste Unitários para garatir o funcionamento correto do sistema após alterações no código.

# Especificações Técnicas
- **Front End:** [Nuxt](https://nuxtjs.org/) com [Vuetifyjs](https://vuetifyjs.com/en/)  como framework de UI;
- **API:** ASP.NET Core e Entity framework;
- **Banco de Dados:** Postgres;
- **Idioma de escrita do código:** Inglês;

# Dependências dos Projetos
## Core
Nenhuma Dependência

## Api
- **AutoMapper**: Simples mapeamento entre objetos;
- **FluentValidation**: Validação inicial de objetos;
- **Swashbuckle**: Interface gráfica para a API (através da rota: /swagger/index.html);
- **Hellang.Middleware.ProblemDetails**: Middleware para padrão de tratamento de erros, usando a especificação RFC7807;
- **Microsoft.EntityFrameworkCore.Tools**: Ferramenta opicional para lidar com scripts do banco no console do Visual Studio.

## Persistence
- **EntityFrameworkCore**: ORM do banco de dados;
- **Npgsql.EntityFrameworkCore.PostgreSQL**: EFCore provider, que permite o uso do ORM com o postgres;
- **EFCore.NamingConventions**: Convenção de nomes de tabelas e colunas para o EFCore.

## Api
- **xunit**: Framework de testes;
- **FakeItEasy**: Biblioteca de Mock para objetos.

# Instruções de Instalação

Banco de Dados - Escolher entre dotnet-ef ou Console do Visual Studio
#### dotnet-ef
Para instalar a ferramenta dotnet-ef, rode o comando e reinicie o terminal
```sh
dotnet tool install --global dotnet-ef
```

Na pasta do projeto, para gerar o banco rode os comandos
```sh
cd src
dotnet-ef database update -s EdTech.Api -p EdTech.Persistence
```

#### Console do Visual Studio
Com o projeto aberto no Visual Studio, para gerar o banco rode o comando
```sh
Update-Database
```