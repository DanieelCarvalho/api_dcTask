# Gerenciador de Tarefas API

Esta é uma API RESTful para gerenciamento de usuários e tarefas, que permite criar contas de usuários, autenticar, e gerenciar tarefas com status variados. A API é desenvolvida em C# com ASP.NET Core e utiliza o padrão de Repositório, AutoMapper e Autenticação JWT.
## **Funcionalidades**

- **Cadastro e Login de Usuários**: Permite criação de contas e autenticação de usuários via JWT.
- **Gerenciamento de Tarefas**:  Criação, atualização, listagem e exclusão de tarefas.
- **Status de Tarefas**:  Gerenciamento dos status das tarefas, incluindo "Em andamento", "Concluída", "Pendente" e "Em atraso".
- **Paginação e Busca de Tarefas**: Suporte para listar tarefas com paginação e busca por palavras-chave.

## **Tecnologias Utilizadas**

- **C# e ASP.NET Core** - Framework principal para construção da API.
- **Entity Framework Core** - ORM para manipulação de dados.
- **AutoMapper** - Simplificação do mapeamento de objetos com DTOs.
- **JWT (JSON Web Token)** - Autenticação e controle de acesso.
- **Microsoft Identity** - Gerenciamento de identidade e autenticação.

