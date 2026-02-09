# 🚀 Guia de Execução Rápida

## Pré-requisitos

- ✅ .NET 10.0 SDK instalado
- ✅ Visual Studio Code, Visual Studio ou Rider
- ✅ Git (para clonar se necessário)

---

## Passo 1: Restaurar Dependências

Na raiz do projeto, execute:

```bash
dotnet restore
```

Isso irá:
- Baixar NUnit, FluentAssertions, Moq e outras dependências
- Restaurar pacotes NuGet

---

## Passo 2: Construir a Solução

```bash
dotnet build
```

**Saída esperada:**
```
Build succeeded. X warnings, 0 errors
```

Se houver erros de referência circular, verifique que:
- ✅ Project references estão corretos
- ✅ Não há dependências circulares

---

## Passo 3: Executar os Testes

### Executar Todos os Testes

```bash
cd WebApplicationReviewTest.Test
dotnet test
```

**Saída esperada:**
```
Test run for D:\...\WebApplicationReviewTest.Test.dll(.NETCoreApp,Version=v10.0)
Microsoft (R) Test Execution Command Line Tool Version 17.11.1

Passed: 26
Failed:  0
Skipped: 0
```

### Executar Testes Específicos

```bash
# Apenas testes de UserService
dotnet test --filter "Class=UserServiceTests"

# Apenas testes de Repositórios
dotnet test --filter "Category=Repositories"

# Com mais verbosidade
dotnet test --verbosity normal
```

---

## Passo 4: Executar a Aplicação

### opção 1: Via dotnet CLI

```bash
cd WebApplicationReviewTest
dotnet run
```

### Option 2: Via Visual Studio/Rider

1. Abra o projeto
2. Clique em "Run" ou pressione `F5`
3. A aplicação irá iniciar no `https://localhost:5001`

**Saída esperada:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to stop, Ctrl+Break for shutdown.
```

---

## Passo 5: Testar os Endpoints

### Via VS Code Rest Client

1. Instale a extensão "REST Client" (`humao.rest-client`)
2. Abra o arquivo [WebApplicationReviewTest/test-api.http](./WebApplicationReviewTest/test-api.http)
3. Clique no botão "Send Request" em cada bloco

### Via curl

```bash
# Health check
curl -k https://localhost:5001/

# Register user
curl -k -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"username":"test","email":"test@test.com","password":"pass123"}'

# Login
curl -k -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"test","password":"pass123"}'
```

### Via Postman

1. Importe a coleção (ou crie manualmente)
2. Endpoints base: `https://localhost:5001/api`
3. Testes básicos:
   - POST `/auth/register`
   - POST `/auth/login`
   - GET `/users`
   - POST `/users/{id}/jobs`

---

## Passo 6: Validar Arquitetura

Execute o script de validação:

```bash
# Verificar estrutura de pastas
./validate-structure.sh

# Ou manualmente:
ls -la WebApplicationReviewTest*/
```

**Estrutura esperada:**
```
✅ WebApplicationReviewTest/              (Camada Web)
✅ WebApplicationReviewTest.Domain/       (Entidades + Interfaces)
✅ WebApplicationReviewTest.Aplication/   (Services + DTOs)
✅ WebApplicationReviewTest.Infra/        (DbContext + Repositories)
✅ WebApplicationReviewTest.Test/         (Testes Unitários)
```

---

## Passo 7: Validar Code Review Points

Use uma destas abordagens para identificar os pontos de melhoria:

### Busca Manual
```bash
# Encontrar todos os "TODO"
grep -r "TODO" . --include="*.cs"

# Contar issues
grep -r "TODO: ISSUE" . --include="*.cs" | wc -l
```

**Saída esperada:**
```
17 TODO: ISSUE comments encontrados
```

### Leitura de Documentos
1. [README.md](./README.md) - Visão geral
2. [CODE_REVIEW_CHECKLIST.md](./CODE_REVIEW_CHECKLIST.md) - Problemas específicos
3. [DETAILED_ANALYSIS.md](./DETAILED_ANALYSIS.md) - Análise profunda

---

## Troubleshooting

### ❌ Erro: "Project not found"
```
Solution: Execute na raiz do projeto onde está WebApplicationReviewTest.slnx
cd d:\Repositorios\...\WebApplicationReviewTest
```

### ❌ Erro: "The resource cannot be found."
```
Solution: Rodando aplicação mas testando endpoint errado
Correto: https://localhost:5001/api/users
Teste primeiro: GET /api/users
```

### ❌ Erro: "SSL certificate problem"
```
Solução (apenas testes local):
curl -k ...  # Adione -k para ignorar certificado

Ou use HTTP em desenvolvimento:
No launchSettings.json, remova HTTPS
```

### ❌ Erro: "Port 5001 already in use"
```
Solução 1: Kill o processo
netstat -ano | findstr :5001
taskkill /PID <PID> /F

Solução 2: Usar porta diferente
dotnet run --urls="https://localhost:5002"
```

### ❌ Testes falhando
```
Verifique:
1. NUnit está instalado: dotnet package list | grep NUnit
2. FluentAssertions: dotnet package list | grep FluentAssertions
3. Moq: dotnet package list | grep Moq

Se faltam, execute:
dotnet add package NUnit
dotnet add package FluentAssertions
dotnet add package Moq
```

---

## 📊 Checklist de Validação

- [ ] `dotnet restore` executed successfully
- [ ] `dotnet build` compiled without errors
- [ ] `dotnet test` passed all 26 tests
- [ ] Application started on `https://localhost:5001`
- [ ] Can call `GET /api/users` endpoint
- [ ] Can register new user via `POST /api/auth/register`
- [ ] Can login via `POST /api/auth/login`
- [ ] Can create jobs via `POST /api/users/{id}/jobs`
- [ ] README.md is readable
- [ ] CODE_REVIEW_CHECKLIST.md shows 17 issues
- [ ] All dependencies are installed

---

## 🎯 Próximos Passos

1. **Ler Documentação**
   - [ ] README.md
   - [ ] CODE_REVIEW_CHECKLIST.md
   - [ ] DETAILED_ANALYSIS.md

2. **Explorar Código**
   - [ ] Domain Layer - Entidades
   - [ ] Application Layer - Services
   - [ ] Infrastructure Layer - Repositories
   - [ ] Web Layer - Controllers
   - [ ] Test Layer - Unit Tests

3. **Identificar Problemas**
   - [ ] Problemas de Segurança
   - [ ] Problemas de Performance
   - [ ] Problemas de Arquitetura
   - [ ] Oportunidades de Melhoria

4. **Propor Soluções**
   - [ ] Criar branches para melhorias
   - [ ] Implementar correções
   - [ ] Adicionar testes
   - [ ] Documentar changes

---

## 📈 Métricas

| Métrica | Valor |
|---------|-------|
| Total de Classes | 20+ |
| Total de Interfaces | 3 |
| Linhas de Código | ~1500 |
| Testes Unitários | 26 |
| Cobertura de Testes | ~82% |
| Pontos de Melhoria | 17 |
| Controllers | 3 |
| Services | 3 |
| Repositories | 2 |

---

## 🔗 Referências Rápidas

- **Documentação .NET**: https://docs.microsoft.com/en-us/dotnet/
- **Entity Framework Core**: https://docs.microsoft.com/en-us/ef/core/
- **ASP.NET Core**: https://docs.microsoft.com/en-us/aspnet/core/
- **NUnit**: https://nunit.org/
- **FluentAssertions**: https://fluentassertions.com/
- **OWASP**: https://owasp.org/

---

## 💬 Dúvidas Frequentes

**P: Por que usar InMemoryDatabase?**
R: Para testes rápidos e sem dependências externas. Produção deve usar SQL Server/PostgreSQL.

**P: Por que as senhas estão em texto plano?**
R: Intencionalmente para demonstrar vulnerabilidade de segurança em code review.

**P: Preciso de JWT implementado?**
R: Sim! Esse é um dos principais pontos de melhoria a implementar.

**P: Posso usar o banco de dados real?**
R: Sim! Modifique Program.cs para usar `UseSqlServer()` ao invés de `UseInMemoryDatabase()`.

---

**Status**: ✅ Pronto para Code Review
**Última Atualização**: 2026-02-05
**Versão**: 1.0
