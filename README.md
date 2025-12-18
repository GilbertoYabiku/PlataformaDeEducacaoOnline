# ** Plataforma de Educação Online**

## :trophy: **1. Apresentação**

Este projeto é uma entrega do MBA DevXpert Full Stack .NET referente ao **módulo 3**.
O objetivo principal Desenvolver uma plataforma educacional online com múltiplos bounded contexts, aplicando DDD, TDD, CQRS e padrões arquiteturais para gestão eficiente de conteúdos educacionais, alunos e processos financeiros, através de uma interface WebAPI.


### :notebook: **Autores**
---

- :white_check_mark: Gilberto Moshim Yabiku Junior - @junmoriyama3d

## :gear: **2. Proposta do Projeto**

O projeto consiste em:

- **API RESTful:** Exposição dos recursos da da gestão da plataforma de educação online.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores, vendedores e clientes (JWT para API RESTful).
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM Entity Framework Core.

## :gear: **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C# 13
- **Frameworks:**
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server / SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Documentação da API:** Swagger

## :gear: **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

```
|-- src
|   |-- Alunos
|   |   |-- PlataformaDeEducacaoOnline.Alunos.Application        → Aplicação de validações e regras de negócio.
|   |   |-- PlataformaDeEducacaoOnline.Alunos.Data               → Mapeamento de modelos de dados, configuração do EF Core e seed do banco de dados.
|   |   |-- PlataformaDeEducacaoOnline.Alunos.Domain             → Definição de entidades referentes ao contexto.
|   |-- Conteúdos
|   |   |-- PlataformaDeEducacaoOnline.Conteudos.Application     → Aplicação de validações e regras de negócio.
|   |   |-- PlataformaDeEducacaoOnline.Conteudos.Data            → Mapeamento de modelos de dados, configuração do EF Core e seed do banco de dados.
|   |   |-- PlataformaDeEducacaoOnline.Conteudos.Domain          → Definição de entidades referentes ao contexto.
|   |-- Financeiro
|   |   |-- PlataformaDeEducacaoOnline.Financeiro.AntiCorruption → Camada de conexão com serviços externos.
|   |   |-- PlataformaDeEducacaoOnline.Financeiro.Application    → Aplicação de validações e regras de negócio.
|   |-- PlataformaDeEducacaoOnline.API                           → API RESTful.
|   |-- PlataformaDeEducacaoOnline.Core                          → Classes comuns a todos os contextos.
|   |-- Testes
|   |   |-- PlataformaDeEducacaoOnline.Tests                     → Projeto de testes.
|-- .gitignore                                                   → Confguração de quais arquivos o Git não deve versionar.
|-- FEEDBACK.md                                                  → Arquivo para feedback dos avaliadores
|-- PlataformaDeEducacaoOnline.sln                               → Solution do projeto
|-- README.md                                                    → Arquivo de documentação
``` 
## :gear: **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 9.0 ou superior
- SQL Server / SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**
---
### 1. **Clone o Repositório:**
   - `git clone https://github.com/GilbertoYabiku/mba-devio-modulo3`
   - `cd mba-devio-modulo3`

### 2. Configuração do Banco de Dados:
  
No arquivo `appsettings.json`, configure a string de conexão do SQL Server (caso deseje executar em modo não "development"). Para execução em modo "Development" (debug), basta executar o projeto (irá subir uma instancia do `SQLite`).

Execute o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

### 3. **Executar a API:**
   - a partir da pasta clonada do projeto, abra o prompt de comando e digite:
   - `cd src\PlataformaDeEducacaoOnline.API`
   - `dotnet run --environment=Development`
   - Abra o browser e acesse a documentação da API em: https://localhost:7094/swagger

## :gear: **7. Instruções de Configuração**

**JWT para API:** As chaves de configuração do JWT estão no arquivo `appsettings.{environment}.json`.

**Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core (Não é necessário aplicar o comando update-database devido a configuração do projeto)

## :gear: **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em através do link [https://localhost:7094/swagger](https://localhost:7094/swagger)

## :white_check_mark: **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações dos instrutores e deverá ser modificado apenas por eles.
