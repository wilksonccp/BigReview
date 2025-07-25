# TaskManager.Api

API RESTful para gerenciamento de tarefas simples, desenvolvida com ASP.NET Core como parte do roadmap de aprendizado focado na stack .NET + AWS (vaga NTT Data).

---

## ?? Tecnologias Utilizadas

- .NET 8 / ASP.NET Core Web API
- C#
- FluentValidation
- Injeção de dependência (Service Layer)
- Armazenamento em memória (será trocado por EF Core)
- Swagger para testes

---

## ?? Funcionalidades

- [x] Criar tarefa
- [x] Listar todas as tarefas
- [x] Buscar tarefa por ID
- [x] Atualizar tarefa
- [x] Remover tarefa
- [x] Validação com FluentValidation
- [ ] Persistência com Entity Framework Core *(em andamento)*

---

## ?? Endpoints

| Método | Rota             | Descrição                 |
|--------|------------------|---------------------------|
| GET    | /api/task        | Lista todas as tarefas    |
| GET    | /api/task/{id}   | Retorna uma tarefa        |
| POST   | /api/task        | Cria uma nova tarefa      |
| PUT    | /api/task/{id}   | Atualiza o título         |
| DELETE | /api/task/{id}   | Remove uma tarefa         |

---

## ?? Testes

A API pode ser testada via:
- Swagger (interface automática)
- Postman ou Thunder Client

---

## ?? Observações

Esse projeto faz parte da **Fase 1** do meu roadmap de estudos para atuar com back-end moderno em .NET. Estou usando ele como base para praticar boas práticas de arquitetura, testes, deploy e integração com nuvem (próximas fases).

---

## ?? Licença

MIT – Use à vontade!
