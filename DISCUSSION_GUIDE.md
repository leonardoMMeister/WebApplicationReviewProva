# 💬 Guia de Discussão - Code Review

**Facilitador de Conversa para Code Review**

Utilize este documento para guiar discussões produtivas durante a análise desta aplicação.

---

## 🎯 Estrutura de Discussão Recomendada

### Fase 1: Compreensão (15 minutos)
```
Perguntas iniciais para o revisor:
1. "Qual é o propósito principal desta aplicação?"
2. "Qual padrão arquitetônico você identifica?"
3. "Quantas camadas tem?"
4. "Que frameworks estão sendo usados?"

Próximo passo: Se não souber, ler README.md
```

### Fase 2: Exploração (30 minutos)
```
Perguntas de exploração:
1. "Como a aplicação flui do controller até o banco?"
2. "Como os testes validam a funcionalidade?"
3. "Qual seria o impacto de deletar User.cs?"
4. "Como a DI está configurada?"

Próximo passo: Executar QUICK_START.md
```

### Fase 3: Análise (45 minutos)
```
Perguntas analíticas:
1. "Quais são os maiores riscos de segurança?"
2. "Quais são os gargalos de performance?"
3. "Qual seria o refactoring mais urgente?"
4. "Como melhoraria a validação?"

Próximo passo: Ler CODE_REVIEW_CHECKLIST.md
```

### Fase 4: Proposição (30 minutos)
```
Perguntas propositivas:
1. "Como implementaria JWT?"
2. "Como adicionaria paginação?"
3. "Qual seria sua estratégia de logging?"
4. "Como estruturaria testes de integração?"

Próximo passo: Criar plano de ação
```

---

## 🔐 Tópico 1: Segurança

### Questões Chave

**1. Senhas em Plaintext**
```
Pergunta básica:
"Qual é o problema com armazenar senhas como texto?"

Resposta esperada:
"Se o banco vazar, todas as senhas são expostas"

Pergunta de aprofundamento:
"Como você as armazenaria?"

Discussão:
- BCrypt vs Bcrypt.Net
- Hashing vs Encryption
- Salt rounds (10+ é seguro)
```

**2. Sem Autenticação**
```
Pergunta:
"O que acontece se chamar GET /api/users sem autenticação?"

Resposta esperada:
"Obtém lista de TODOS os usuários - severidade crítica"

Pergunta:
"Como você implementaria autenticação?"

Discussão:
- JWT tokens
- Session cookies
- OAuth2/OIDC
- Refresh tokens
```

**3. Sem Autorização**
```
Pergunta:
"Um usuário pode deletar jobs de outro usuário?"

Resposta esperada:
"Sim, não há validação de ownership"

Pergunta:
"Como adicionar verificação?"

Discussão:
- Policy-based authorization
- Role-based access control (RBAC)
- Attribute-based access control (ABAC)
```

**4. Timing Attacks**
```
Pergunta:
"Qual é o problema dessa comparação?"
    user.Password == inputPassword

Resposta esperada:
"Diferente de tempo de execução = pode enumerar"

Pergunta:
"Como corrigir?"

Discussão:
- CryptographicOperations.FixedTimeEquals
- Sempre comparar mesmo tempo
```

### Discussão Facilitada

```
Facilitador: "Vamos focar em segurança. O que você vê de
             crítico em User.cs?"

Revisor:     "As senhas estão em texto plano..."

Facilitador: "Exatamente. Qual seria seu primeiro passo
             para consertar isso?"

Revisor:     "Implementar hash com bcrypt..."

Facilitador: "Ótimo. Isso quebrava os testes? Como você
             atualizaria os testes?"

Revisor:     "Mocar o hash, usar valores esperados..."

Facilitador: "Perfeito. Qual seria seu próximo problema?"
```

---

## ⚡ Tópico 2: Performance

### Questões Chave

**1. Sem Paginação**
```
Pergunta:
"O que acontece ao chamar GET /api/users com 1 milhão de registros?"

Resposta esperada:
"Traz todos 1M para memória, aplicação cai ou fica muito lenta"

Pergunta:
"Como você implementaria paginação?"

Discussão:
- Skip() e Take() em LINQ
- Metadados: Total, CurrentPage, PageSize
- Link headers (RFC 5988)
```

**2. Sem Índices**
```
Pergunta:
"Qual seria o tempo de query em GetByUsernameAsync com 10M de users?"

Resposta esperada:
"Full table scan, muito lento (segundos)"

Pergunta:
"Qual índice você criaria?"

Discussão:
- CREATE UNIQUE INDEX ON User(Username)
- Índices compound
- Query performance analysis
```

**3. N+1 Queries**
```
Pergunta:
"Quais queries são executadas aqui?
foreach(var user in users) { 
    Console.WriteLine(user.Jobs.Count); 
}"

Resposta esperada:
"1 query users + N queries de jobs = N+1"

Pergunta:
"Como você evitaria isso?"

Discussão:
- Include/ThenInclude no EF
- Eager loading vs lazy loading
- Projections
```

### Discussão Facilitada

```
Facilitador: "Imagina que você tem 10 milhões de jobs.
             Um usuário chama GET /api/users. Qual seria
             o problema?"

Revisor:     "Puxaria todos 10 milhões para memória?"

Facilitador: "Exatamente! Que solução você proporia?"

Revisor:     "Paginação... com Skip e Take?"

Facilitador: "Correto. Como você retornaria os metadados
             de paginação?"

Revisor:     "Um objeto wrapper com total count, items..."

Facilitador: "Perfeito! Qual seria o esquema?"
```

---

## 🏗️ Tópico 3: Arquitetura

### Questões Chave

**1. Métodos Muito Grandes**
```
Pergunta:
"O método CreateJobAsync tem quantas responsabilidades?"

Resposta esperada:
"Cria job, valida, registra - 3 responsabilidades"

Pergunta:
"Como você quebraria?"

Discussão:
- Single Responsibility Principle
- Extract methods
- Separar validação em classe própria
```

**2. Falta de Validação**
```
Pergunta:
"Se você chamar CreateUser com username vazio, o que acontece?"

Resposta esperada:
"Cria usuário inválido no banco"

Pergunta:
"Como você validaria?"

Discussão:
- FluentValidation
- Data Annotations
- Validação em múltiplas camadas
```

**3. Status como String**
```
Pergunta:
"Por que Status = 'Pendig' (typo) é um problema?"

Resposta esperada:
"Não há garantia de tipo, erros em runtime"

Pergunta:
"Como você estruturaria melhor?"

Discussão:
- Enums em C#
- Transições válidas de estado
- State pattern
```

### Discussão Facilitada

```
Facilitador: "Olhe para CreateJobAsync. Quantas coisas
             ela faz?"

Revisor:     "Cria... valida... loga?"

Facilitador: "Correto. O que seria ideally separado?"

Revisor:     "Validação em ValidateJobDto, logging..."

Facilitador: "Como você teria refatorado isso?"

Revisor:     "Métodos privados: ValidateJob, LogCreation..."

Facilitador: "Ótimo. Qual padrão conhecemos para isso?"
```

---

## 🧪 Tópico 4: Testes

### Questões Chave

**1. Cobertura Incompleta**
```
Pergunta:
"Quais testes estão faltando para UserService?"

Resposta esperada:
"Criar usuário duplicado, email inválido..."

Pergunta:
"Como você testaria erro?"

Discussão:
- Assert.ThrowsAsync
- Expected exceptions
- Cenários de erro
```

**2. Sem Testes de Integração**
```
Pergunta:
"Podemos ter bugs em UserService + UserRepository juntos?"

Resposta esperada:
"Sim, testes de integração descobririam"

Pergunta:
"Como testar sem banco de verdade?"

Discussão:
- WebApplicationFactory
- In-Memory database
- Integration test patterns
```

**3. Testes da API**
```
Pergunta:
"Controller testado? Como testaria autenticação?"

Resposta esperada:
"Não está testado. Usaria HttpClient custom..."

Pergunta:
"Qual seria seu teste do /login?"

Discussão:
- Mock authentication service
- Assert response structure
- Status codes
```

### Discussão Facilitada

```
Facilitador: "Temos 26 testes aqui. São suficientes?"

Revisor:     "Faltam casos de erro... edge cases..."

Facilitador: "Qual seria seu teste mais importante?"

Revisor:     "Autenticar com senha errada?"

Facilitador: "Bom. Como você estruturaria arquitetura
             de teste?"

Revisor:     "Separar: Unit, Integration, E2E..."

Facilitador: "Exatamente. Qual seria cada uma?"
```

---

## 💡 Tópico 5: Design Patterns

### Questões Chave

**1. Repository Pattern**
```
Pergunta:
"Por que usar Repository ao invés de acessar DbContext direto?"

Resposta esperada:
"Abstração, testabilidade, mudança de BD não quebra testes"

Pergunta:
"O Repository está bem implementado?"

Discussão:
- Unit of Work pattern
- Generic repositories
- Specification pattern
```

**2. Dependency Injection**
```
Pergunta:
"Olhe para Program.cs. O que você vê?"

Resposta esperada:
"Registrando services em um lugar"

Pergunta:
"Qual seria o risco se não usasse DI?"

Discussão:
- Tight coupling
- Testes com dependências reais
- Mudanças cascata
```

**3. DTOs**
```
Pergunta:
"Por que usar UserDto em vez de User direto?"

Resposta esperada:
"Separa domínio de apresentação"

Pergunta:
"Qual seria o problema se expusesse User?"

Discussão:
- Exposição de dados sensíveis
- Evolução independente
- Serialização circular
```

---

## 🎯 Simulação de Code Review Real

### Cenário 1: O Iniciante

```
Revisor: "Oi, achei muito código. Por onde começo?"

Facilitador: "Ótima pergunta! Qual é o primeiro erro de
             segurança que você vê?"

Revisor: "As... as senhas estão em texto plano?"

Facilitador: "Exato! Como você corrigiria isso?"

Revisor: "Com bcrypt?"

Facilitador: "Certo! Qual seria seu passo 1, 2, 3?"

Revisor: "1. Instalar NuGet de bcrypt
          2. Alterar User.cs
          3. Atualizar service..."

Facilitador: "Perfeito! Você está pronto para começar!"
```

### Cenário 2: O Intermédio

```
Revisor: "Identifiquei múltiplos problemas:
          - Sem autenticação
          - Sem paginação
          - Sem validação
          
          Por onde começo?"

Facilitador: "Ótima listagem! Qual você acha MAIS crítico?"

Revisor: "Autenticação, porque sem ela qualquer um
          acessa tudo"

Facilitador: "Concordo. Como você implementaria?"

Revisor: "JWT com roles. UserService valida, Controller
          usa [Authorize]..."

Facilitador: "E autorização? Como previne que edite outro user?"

Revisor: "Checa se id do token = id da rota"

Facilitador: "Exato! Qual seria seu segundo issue?"
```

### Cenário 3: O Avançado

```
Revisor: "A arquitetura é boa, mas há problemas:

1. Segurança é crítica (17 issues)
2. Repository Pattern está ok mas falta UnitOfWork
3. Service Layer mistura lógica de negócio com validação
4. Tests faltam integração e E2E
5. Falta observabilidade (logging, monitoring)

Qual é minha recomendação global?"

Facilitador: "Excelente análise! Como você prorizaria?"

Revisor: "1º - Segurança (blockers)
          2º - Arquitetura (refactor)
          3º - Tests (confiança)
          4º - Observabilidade (produção)

E propostas concretas:
- JWT + Claims authorization
- Extract validators com FluentValidation
- Repository<T> genérico
- Integration tests com WebApplicationFactory
- Serilog para logging"

Facilitador: "Perfeito! Isso seria um excelente PR."
```

---

## 📋 Roteiro de Conversa Pronto para Usar

### 5 Minutos (Morning Standup)
```
"O que você achou da arquitetura?"
💬 Resposta → "Bem organizada em camadas"

"Qual é o problema mais óbvio?"
💬 Resposta → "Senhas em plaintext"

"Como você iniciaria o refactor?"
💬 Resposta → "Com segurança"
```

### 15 Minutos (Curta Discussão)
```
1. "Entendeu a estrutura?" → 3min
2. "Qual é o problema mais crítico?" → 5min
3. "Como você começaria a corrigir?" → 5min
4. "Próximos passos?" → 2min
```

### 45 Minutos (Code Review Completo)
```
1. Visão geral (5min) → Ler EXECUTIVE_SUMMARY
2. Problemas de segurança (12min) → Discutir 5 issues
3. Problemas de arquitetura (12min) → Propor refactors
4. Testes e cobertura (10min) → Aumentar cobertura
5. Próximas ações (6min) → Criar plano
```

---

## ✨ Boas Práticas de Discussão

### ✅ Faça Assim

```
"Eu vi que aqui... qual é seu pensamento disso?"
"Excelente observação! Como você propõe melhorar?"
"Concordo. Qual seria seu passo prático?"
"Perfeito! Qual seria a razão técnica disso?"
```

### ❌ Evite Assim

```
"Isso está errado!"              → Use "Qual seria uma forma melhor?"
"Deveria saber isso"             → Use "Como aprendeu isso?"
"Vou corrigir por você"          → Use "Quer tentar? Posso ajudar"
"Sem perguntar"                  → Use "Qual era sua intenção aqui?"
```

---

## 🎓 Objetivos de Discussão

### Para o Revisor Aprender
- ✅ Identificar problemas reais
- ✅ Propor soluções concretas
- ✅ Entender trade-offs
- ✅ Conhecer padrões e práticas

### Para o Facilitador Avaliar
- ✅ Pensamento crítico
- ✅ Conhecimento técnico
- ✅ Comunicação clara
- ✅ Humildade e disposição

### Para Ambos Ganharem
- ✅ Melhor código
- ✅ Melhores práticas
- ✅ Conhecimento compartilhado
- ✅ Confiança mútua

---

## 📊 Rubrica de Avaliação

| Aspecto | Iniciante | Intermediário | Avançado |
|---------|-----------|--------------|----------|
| **Identificar Issues** | 5 óbvios | 10+ issues | Todas as 17 |
| **Propor Soluções** | Genéricas | Com detalhes | Implementação ready |
| **Profundidade** | Superficial | Técnica | Arquitetural |
| **Comunicação** | Hesitante | Clara | Estruturada |
| **Tempo** | Precisa Help | Independente | Guia outros |

---

## 🚀 Próximas Discussões

Após completar esta, sugira:

1. **Deep Dive em Segurança**
   - Implementar JWT
   - Padrões de autenticação moderna

2. **Otimização de Performance**
   - Paginação e caching
   - Índices no BD

3. **Arquitetura Avançada**
   - CQRS, Event Sourcing
   - Microserviços

4. **DevOps e Produção**
   - CI/CD, Containerização
   - Observabilidade

---

**Facilitador**: Use este documento para guiar conversas produtivas! 🎓

*Último Atualizado: 05 de Fevereiro de 2026*
