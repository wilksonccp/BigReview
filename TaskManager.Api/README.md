# TaskManager.Api

API RESTful para gerenciamento de tarefas simples, desenvolvida com ASP.NET Core como parte do roadmap de aprendizado focado na stack .NET + AWS (vaga NTT Data).

---

## ?? Tecnologias Utilizadas

- .NET 8 / ASP.NET Core Web API
- C#
- FluentValidation
- Inje��o de depend�ncia (Service Layer)
- Armazenamento em mem�ria (ser� trocado por EF Core)
- Swagger para testes

---

## ?? Funcionalidades

- [x] Criar tarefa
- [x] Listar todas as tarefas
- [x] Buscar tarefa por ID
- [x] Atualizar tarefa
- [x] Remover tarefa
- [x] Valida��o com FluentValidation
- [ ] Persist�ncia com Entity Framework Core *(em andamento)*

---

## ?? Endpoints

| M�todo | Rota             | Descri��o                 |
|--------|------------------|---------------------------|
| GET    | /api/task        | Lista todas as tarefas    |
| GET    | /api/task/{id}   | Retorna uma tarefa        |
| POST   | /api/task        | Cria uma nova tarefa      |
| PUT    | /api/task/{id}   | Atualiza o t�tulo         |
| DELETE | /api/task/{id}   | Remove uma tarefa         |

---

## ?? Testes

A API pode ser testada via:
- Swagger (interface autom�tica)
- Postman ou Thunder Client

---

## ?? Observa��es

Esse projeto faz parte da **Fase 1** do meu roadmap de estudos para atuar com back-end moderno em .NET. Estou usando ele como base para praticar boas pr�ticas de arquitetura, testes, deploy e integra��o com nuvem (pr�ximas fases).

---

## ?? Licen�a

MIT � Use � vontade!
