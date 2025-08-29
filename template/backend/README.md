Ambev.DeveloperEvaluation
Bem-vindo ao projeto de avaliação de desenvolvedor Fullstack Sênior da Ambev.

Este projeto implementa uma API RESTful completa para gerenciar registros de vendas, seguindo o padrão de arquitetura DDD (Domain-Driven Design).

Índice
Tecnologias

Arquitetura

Regras de Negócio

Configuração do Ambiente

Endpoints da API

Testes

Observações Adicionais

Tecnologias
Backend: .NET 8.0, C#

Banco de Dados: PostgreSQL (Docker)

Frameworks: MediatR, Entity Framework Core, NSubstitute (para testes), Faker (para testes)

Controle de Versão: Git

Arquitetura
O projeto segue os princípios da Arquitetura Limpa (Clean Architecture) e DDD (Domain-Driven Design). A separação de projetos é a seguinte:

Core:

Ambev.DeveloperEvaluation.Domain: Contém as entidades e regras de negócio (Sale, SaleItem).

Ambev.DeveloperEvaluation.Application: Lógica da aplicação, com o uso de Commands e Queries pelo MediatR.

Adapters:

Ambev.DeveloperEvaluation.WebApi: A API que expõe os endpoints HTTP.

Ambev.DeveloperEvaluation.ORM: Camada de persistência, usando Entity Framework Core.

Crosscutting:

Ambev.DeveloperEvaluation.Common: Utilitários e funcionalidades compartilhadas.

Ambev.DeveloperEvaluation.IoC: Configuração de injeção de dependência.

Regras de Negócio
As seguintes regras de desconto foram implementadas na entidade SaleItem:

10% de desconto para compras com 4 a 9 itens idênticos.

20% de desconto para compras com 10 a 20 itens idênticos.

Vendas acima de 20 itens não são permitidas.

Configuração do Ambiente
Pré-requisitos:

Docker e Docker Compose instalados.

.NET 8.0 SDK instalado.

Iniciar o Banco de Dados:

Navegue até a pasta backend.

Inicie o contêiner do banco de dados PostgreSQL usando Docker Compose:

docker-compose up -d ambev.developerevaluation.database

Executar Migrações:

Execute a migração do Entity Framework para criar as tabelas no banco de dados. Este comando precisa ser executado na pasta backend:

dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj --startup-project src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj

Executar a Aplicação:

Inicie o projeto a partir da pasta backend:

dotnet run --project src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj

A API estará disponível em https://localhost:5001.

Endpoints da API
[POST] /api/sales: Cria uma nova venda.

[GET] /api/sales: Lista todas as vendas com paginação.

[GET] /api/sales/{id}: Busca uma venda por ID.

[PUT] /api/sales/{id}: Atualiza uma venda existente.

[DELETE] /api/sales/{id}: Cancela uma venda (marca como cancelada).

Testes
O projeto inclui testes unitários, de integração e funcionais na pasta tests/.

Executar todos os testes:

dotnet test

Observações Adicionais
A implementação da publicação de eventos (SaleCreated, SaleModified, etc.), que era um diferencial opcional, foi estruturada na camada de Application e pode ser facilmente integrada com um serviço de mensageria como o Rebus, que é mencionado na documentação de frameworks.md.

Este projeto demonstra proficiência em C#, arquitetura de software, padrões de design, testes automatizados e uso de ferramentas modernas como Docker e Entity Framework Core.