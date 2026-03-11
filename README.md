# NexusIntegration

Serviço de integração intermediário (_middleware_) entre uma **plataforma SaaS em nuvem** e um **ERP on-premise**, construído em **.NET 8** com Clean Architecture.

> **Status:** Em fase de scaffolding inicial — migração do serviço original em NestJS/Node.js.

---

## Visão Geral

O NexusIntegration roda nos servidores do cliente (Windows Server) e expõe uma API REST autenticada que permite à plataforma SaaS ler e manipular dados do ERP de forma segura, rastreável e governada, sem acesso direto ao banco de dados.

```
Plataforma SaaS (Cloud)
          │
          │ HTTPS / JWT (OAuth 2.0 Client Credentials)
          ▼
  NexusIntegration API  ◄── Windows Server do cliente
    ├── Autenticação JWT
    ├── Rate Limiting (Redis)
    ├── Camada de Query (whitelist)
    ├── Camada de Escrita (whitelist)
    └── Auditoria (PostgreSQL local)
          │
          │ Driver Oracle (pool de conexões)
          ▼
  Banco de dados do ERP (Oracle)
```

### Casos de uso do MVP

- **Aprovação de pedidos comerciais** — fluxo de aprovação com desconto ou valor acima do limite configurado
- **Meta de compras com alçada** — acionamento automático de aprovação ao ultrapassar meta mensal

---

## Arquitetura

O projeto segue **Clean Architecture** com organização em **Monolito Modular**.

### Estrutura de projetos

```
src/
├── NexusIntegration.Api/            # Controllers, DI, middlewares, Swagger, JWT
├── NexusIntegration.Application/    # Use cases, DTOs, interfaces (ports)
├── NexusIntegration.Domain/         # Entidades, regras de negócio, erros de domínio
├── NexusIntegration.Infrastructure/ # Repositórios Oracle (Dapper), PostgreSQL (EF Core), cache
└── NexusIntegration.Shared/         # Exceções customizadas, utilitários cross-cutting
```

### Regra de dependência

```
Api → Application, Infrastructure, Shared
Application → Domain, Shared
Infrastructure → Domain, Shared
Domain → (sem dependências)
Shared → (sem dependências)
```

### Módulos de negócio planejados

| Módulo | Descrição |
|---|---|
| `Invoices` | Notas fiscais de entrada |
| `Suppliers` | Fornecedores |
| `Orders` | Pedidos comerciais |
| `Purchase` | Pedidos de compra e metas |
| `GenericCrud` | Motor de query/insert genérico com whitelist |
| `Platform/Auth` | Autenticação JWT, OAuth 2.0 client credentials |
| `Platform/ApiClients` | Gestão de clientes API |
| `Platform/Audit` | Log de auditoria |
| `Platform/RateLimit` | Rate limiting por cliente |

---

## Tecnologias

| Tecnologia | Versão | Uso |
|---|---|---|
| .NET | 8.0 | Framework base |
| ASP.NET Core Web API | 8.0 | Camada HTTP |
| Swashbuckle / Swagger | 6.6.2 | Documentação da API |
| Entity Framework Core | — | PostgreSQL (api_clients, audit, tokens) |
| Oracle Managed Data Access + Dapper | — | Acesso ao banco do ERP |
| PostgreSQL | — | Banco próprio do serviço |
| Redis | — | Cache de queries e rate limiting |
| JWT Bearer | — | Autenticação OAuth 2.0 |
| FluentValidation | — | Validação de DTOs |
| xUnit + FluentAssertions | — | Testes unitários e de integração |

---

## Pré-requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- Acesso ao banco Oracle do ERP — ambiente do cliente
- PostgreSQL — banco local do serviço
- Redis — cache e rate limiting

---

## Como executar

```bash
# Clonar o repositório
git clone <url-do-repositório>
cd NexusIntegration

# Restaurar dependências
dotnet restore NexusIntegration.sln

# Build
dotnet build NexusIntegration.sln

# Executar a API (ambiente de desenvolvimento)
cd src/NexusIntegration.Api
dotnet run
```

A API sobe em:

- `http://localhost:5023`
- `https://localhost:7286`
- Swagger UI: `https://localhost:7286/swagger` _(somente em Development)_

---

## Variáveis de ambiente

Crie um arquivo `appsettings.Development.json` ou configure as variáveis de ambiente:

```json
{
  "Oracle": {
    "User": "",
    "Password": "",
    "ConnectionString": ""
  },
  "Database": {
    "Host": "",
    "Port": 5432,
    "Username": "",
    "Password": "",
    "Database": ""
  },
  "Jwt": {
    "Secret": "",
    "ExpiresIn": "1h"
  },
  "Redis": {
    "Password": ""
  },
  "Integration": {
    "WebhookUrl": "",
    "AllowedOrigins": ""
  }
}
```

---

## Estrutura do repositório

```
NexusIntegration/
├── NexusIntegration.sln
├── nexus-integration.code-workspace
└── src/
    ├── NexusIntegration.Api/
    ├── NexusIntegration.Application/
    ├── NexusIntegration.Domain/
    ├── NexusIntegration.Infrastructure/
    └── NexusIntegration.Shared/
```

---

## Convenções do projeto

- **Commits:** padrão [Conventional Commits](https://www.conventionalcommits.org/) com assunto em português (pt-BR)
- **Branch principal:** `develop`
- **Arquitetura:** Clean Architecture + Monolito Modular

---

## Contexto

Este projeto é a **reescrita em .NET 8** de um serviço originalmente desenvolvido em NestJS/Node.js. O serviço original é funcional e está em produção; este repositório representa a migração planejada para a stack .NET.
