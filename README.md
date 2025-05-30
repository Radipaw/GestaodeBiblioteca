# Sistema de Gestão de Biblioteca

##integrantes
Enzo Kikuchi, Igor Acatauassu, Leandro Sarkis, Mario Anijar, Pedro Monteiro

Este é um sistema para gerenciamento de usuários e empréstimos em uma biblioteca. Permite o cadastro, atualização, consulta e inativação de usuários, além do controle de empréstimos de itens do acervo.

##Funcionalidades

- Cadastro de novos usuários com validações de CPF e campos obrigatórios
- Consulta de usuários por nome, CPF, ID ou matrícula
- Atualização de informações de usuários com controle de versão
- Inativação lógica de usuários (com histórico e motivo)
-  Registro de empréstimos com validação de regras e prazos

##Tecnologias Utilizadas

- **.NET 7 / 8**
- **ASP.NET Core Blazor**
- **Entity Framework Core**
- **PostgreSQL (via Npgsql)**
- **xUnit** para testes
- **FluentValidation** para validações
- **In-Memory Database** (nos testes unitários)

## Testes

O projeto possui dois tipos de testes:

- **Testes Unitários:** Validam regras de negócio e validações (ex: CPF inválido, nome vazio).
- **Testes de Integração:** Testam a interação com o banco de dados (PostgreSQL ou In-Memory).

### Executar Testes

Você pode rodar os testes com o seguinte comando:

```bash
dotnet test
