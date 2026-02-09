# WebApplicationReviewTest - Aplicação para Code Review

Aplicação ASP.NET Core desenvolvida especificamente para um processo de code review com múltiplos padrões de análise e pontos de melhoria intencionais.

## 📋 Estrutura do Projeto

```
WebApplicationReviewTest/
├── WebApplicationReviewTest/              # Camada Web (API)
│   ├── Controllers/
│   │   ├── AuthController.cs              # Endpoints de autenticação
│   │   ├── UsersController.cs             # CRUD de usuários
│   │   └── JobsController.cs              # CRUD de jobs
│   ├── Program.cs                         # Configuração de DI
│   └── WebApplicationReviewTest.csproj
│
├── WebApplicationReviewTest.Domain/       # Camada de Domínio
│   ├── Entities/
│   │   ├── User.cs                        # Entidade de Usuário
│   │   └── Job.cs                         # Entidade de Job
│   ├── Interfaces/
│   │   ├── IUserRepository.cs
│   │   ├── IJobRepository.cs
│   │   └── IAuthenticationService.cs
│   └── WebApplicationReviewTest.Domain.csproj
│
├── WebApplicationReviewTest.Aplication/   # Camada de Aplicação
│   ├── DTOs/
│   │   ├── UserDto.cs                     # DTO para User
│   │   └── JobDto.cs                      # DTO para Job
│   ├── Services/
│   │   ├── UserService.cs                 # Serviço de Usuários
│   │   ├── JobService.cs                  # Serviço de Jobs
│   │   └── AuthenticationService.cs       # Serviço de Autenticação
│   └── WebApplicationReviewTest.Aplication.csproj
│
├── WebApplicationReviewTest.Infra/        # Camada de Infraestrutura
│   ├── Data/
│   │   └── ApplicationDbContext.cs        # DbContext do EF Core
│   ├── Repositories/
│   │   ├── UserRepository.cs
│   │   └── JobRepository.cs
│   └── WebApplicationReviewTest.Infra.csproj
│
├── WebApplicationReviewTest.Test/         # Camada de Testes
│   ├── Services/
│   │   ├── UserServiceTests.cs
│   │   ├── JobServiceTests.cs
│   │   └── AuthenticationServiceTests.cs
│   ├── Repositories/
│   │   ├── UserRepositoryTests.cs
│   │   └── JobRepositoryTests.cs
│   └── WebApplicationReviewTest.Test.csproj
│
└── CODE_REVIEW_CHECKLIST.md              # Documento com pontos de melhoria
```

## 🚀 Como usar a aplicação

### Pré-requisitos
- .NET 10.0 ou superior
- Visual Studio Code, Visual Studio ou JetBrains Rider

### 1. Clonar e Restaurar Dependências

```bash
cd WebApplicationReviewTest
dotnet restore
```

### 2. Construir a Solução

```bash
dotnet build
```

### 3. Executar a Aplicação

```bash
cd WebApplicationReviewTest
dotnet run
```

A API estará disponível em: `https://localhost:5001`

### 4. Executar os Testes Unitários

```bash
cd WebApplicationReviewTest.Test
dotnet test
```

Para executar com mais detalhes:

```bash
dotnet test --verbosity normal
```

## 🔌 Endpoints Disponíveis

### Authentication

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "testuser",
  "password": "password123"
}
```

**Resposta (200 OK):**
```json
{
  "success": true,
  "message": "Login realizado com sucesso",
  "user": {
    "id": 1,
    "username": "testuser",
    "email": "test@example.com",
    "createdAt": "2026-02-05T10:00:00Z"
  }
}
```

#### Register
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "newuser",
  "email": "newuser@example.com",
  "password": "password123"
}
```

### Users

#### Listar Todos os Usuários
```http
GET /api/users
```

#### Obter Usuário por ID
```http
GET /api/users/1
```

#### Criar Novo Usuário
```http
POST /api/users
Content-Type: application/json

{
  "username": "novousuario",
  "email": "novo@example.com",
  "password": "senha123"
}
```

#### Atualizar Usuário
```http
PUT /api/users/1
Content-Type: application/json

{
  "username": "usuarioatualizado",
  "email": "atualizado@example.com",
  "password": "novaSenha123"
}
```

#### Deletar Usuário
```http
DELETE /api/users/1
```

### Jobs

#### Listar Jobs do Usuário
```http
GET /api/users/1/jobs
```

#### Obter Job Específico
```http
GET /api/users/1/jobs/1
```

#### Criar Job
```http
POST /api/users/1/jobs
Content-Type: application/json

{
  "title": "Implementar Feature X",
  "description": "Desenvolver a funcionalidade X conforme especificação",
  "dueDate": "2026-03-05T00:00:00Z"
}
```

#### Atualizar Job
```http
PUT /api/users/1/jobs/1
Content-Type: application/json

{
  "title": "Implementar Feature X - Updated",
  "description": "Descrição atualizada",
  "status": "InProgress",
  "dueDate": "2026-03-10T00:00:00Z"
}
```

#### Deletar Job
```http
DELETE /api/users/1/jobs/1
```

## 🔍 Análise de Code Review - Principais Pontos

### 🔴 CRÍTICO - Segurança

1. **Senhas em Texto Plano** - As senhas são armazenadas sem hash
2. **Sem Autenticação/Autorização** - Qualquer pessoa pode acessar qualquer endpoint
3. **Vulnerável a Timing Attacks** - Comparação de senha usa `==`
4. **Sem Rate Limiting** - Sem proteção contra brute force

### 🟠 ALTA - Banco de Dados

5. **Sem Índices** - Queries sem índices podem ser lentas
6. **Status como String** - Deveria ser Enum
7. **Sem Validação no BD** - Campos sem constraints

### 🟡 MÉDIA - Arquitetura

8. **Sem Paginação** - Endpoints retornam todos os registros
9. **Sem Logging** - Sem logs estruturados
10. **Tratamento de Exceção Genérico** - Catch too broad
11. **Sem Validação de Input** - Não valida dados de entrada
12. **Sem Verificação de Duplicatas** - Permite usernames duplicados

## 🧪 Testes

A aplicação inclui 26 testes unitários usando **NUnit** e **FluentAssertions**:

### Testes de Serviço
- ✅ UserServiceTests (5 testes)
- ✅ JobServiceTests (4 testes)
- ✅ AuthenticationServiceTests (5 testes)

### Testes de Repositório
- ✅ UserRepositoryTests (6 testes)
- ✅ JobRepositoryTests (6 testes)

Todos os testes estão em `WebApplicationReviewTest.Test/`

## 📦 Dependências Principais

### Web API
- ASP.NET Core 10.0
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.InMemory

### Application & Domain
- Nenhuma dependência externa (apenas .NET Core)

### Testing
- NUnit 4.1.0
- FluentAssertions 6.12.1
- Moq 4.20.70
- Microsoft.NET.Test.Sdk 17.11.1

## ⚙️ Configuração

### Database
A aplicação usa **In-Memory Database** do Entity Framework Core por padrão (veja `Program.cs`).

Para usar SQL Server, modifique:
```csharp
// Em Program.cs
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=WebApplicationReviewTest;Trusted_Connection=true;"));
```

## 🎯 Objetivos do Code Review

Esta aplicação foi desenvolvida para exercitar análise de código em contextos reais com:

1. ✅ **Múltiplas camadas** - Domain, Application, Infrastructure, Web
2. ✅ **Padrões de design** - Repository, Dependency Injection, DTOs
3. ✅ **Segurança** - Pontos críticos intencionais
4. ✅ **Performance** - Problemas de N+1, sem paginação
5. ✅ **Testes** - Suite de testes unitários
6. ✅ **Código com qualidade variada** - Alguns pontos bons, outros ruins

## 💡 Sugestões para Análise

1. Identifique todos os problemas de segurança
2. Proponha melhorias na arquitetura
3. Sugira refatorações para melhorir legibilidade
4. Identifique possíveis bottlenecks de performance
5. Valide cobertura de testes
6. Sugira padrões e bibliotecas adicionais

## 📄 Documentação Completa

Veja [CODE_REVIEW_CHECKLIST.md](./CODE_REVIEW_CHECKLIST.md) para uma lista detalhada de todos os pontos de melhoria com localizações específicas.

## 📅 Informações

- **Versão**: 1.0
- **Data**: Fevereiro 2026
- **Framework**: ASP.NET Core 10.0
- **Linguagem**: C# 13.0
- **Status**: ✅ Pronto para Code Review

---

**Desenvolvido para demonstrar boas práticas (e anti-padrões intencionais) em desenvolvimento C#**
