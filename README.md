# 🛡️ Seguros.sln

Sistema de gerenciamento de propostas de seguros com arquitetura **DDD + CQRS + Saga Pattern (RabbitMQ)**.

---

## 📂 Estrutura de Pastas

```bash
Seguros.sln
  ├── Infraestructure.Api           # Persistência de dados (DB)
  ├── Program.Api                   # API de entrada (Controllers)
  └── Saga.Orquestrador.Api         # Orquestrador RabbitMQ (Saga Pattern)
```

---

## 🔄 Fluxo de Alto Nível

1. **Program.Api**

   * Recebe requisições HTTP (`PropostaController`).
   * Converte para `PropostaInput`.
   * Publica mensagem no **RabbitMQ** (fila `proposta-fila`).

2. **Saga.Orquestrador.Api**

   * `PropostaSagaWorker` consome a fila.
   * `PropostaProcessor` aplica regras de negócio.
   * Decide se a proposta é **aprovada, rejeitada ou pendente**.
   * Publica eventos ou envia para persistência.

3. **Infraestructure.Api**

   * Responsável pela persistência no **SQL Server**.
   * `PropostaRepository` faz acesso ao DB.
   * `PropostaMap.cs` mapeia entidades ORM.

---

## 🛠️ Manual de Uso

### 🔽 Requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)
* [Postman](https://www.postman.com/downloads/) ou [Insomnia](https://insomnia.rest/download)

---

### ⚙️ Passo a Passo

#### 1. Subir Infra com Docker

Crie um arquivo **`docker-compose.yml`** na raiz do projeto:

```yaml
version: "3.9"

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: seguros_sqlserver
    restart: unless-stopped
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=${SA_PASSWORD}
    ports:
      - "1433:1433"
    volumes:
      - ./data/sqlserver:/var/opt/mssql

  rabbitmq:
    image: rabbitmq:3-management
    container_name: seguros_rabbit
    restart: unless-stopped
    environment:
      - RABBITMQ_DEFAULT_USER=${RABBITMQ_USER}
      - RABBITMQ_DEFAULT_PASS=${RABBITMQ_PASS}
    ports:
      - "5672:5672"
      - "15672:15672"
    volumes:
      - ./data/rabbitmq:/var/lib/rabbitmq
```

Crie também um arquivo **`.env`**:

```env
# SQL Server
SA_PASSWORD=proposta@56

# RabbitMQ
RABBITMQ_USER=guest
RABBITMQ_PASS=guest
```

Suba os serviços:

```bash
docker-compose up -d
```

---

#### 2. Criar o Banco e Tabela

Acesse o container SQL Server:

```bash
docker exec -it seguros_sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P proposta@56!
```

Execute o script:

```sql
CREATE DATABASE homologacao;
GO

USE homologacao;
GO

CREATE TABLE Proposta (
    Id UNIQUEIDENTIFIER PRIMARY KEY,   
    NomeCliente NVARCHAR(100) NOT NULL,
    Valor DECIMAL(18, 2) NOT NULL,   
    Status NVARCHAR(50) NOT NULL DEFAULT 'Em Análise',
    Cpf NVARCHAR(14) NOT NULL,  
    DataNascimento DATE NOT NULL
);
GO
```

---

#### 3. Restaurar Dependências

```bash
dotnet restore
```

#### 4. Subir Projetos

```bash
dotnet run --project Program.Api
dotnet run --project Saga.Orquestrador.Api
dotnet run --project Infraestructure.Api
```

---

### 🚀 Testando com Postman

1. Endpoint para criar proposta:

```http
POST http://localhost:5000/homologacao/api/proposta
Content-Type: application/json

{
  "clienteId": "12345",
  "nomeCliente": "Julio Fazenda",
  "valor": 2500.00,
  "cpf": "123.456.789-00",
  "dataNascimento": "1990-05-21",
  "tipoSeguro": "Automovel"
}
```

```http
POST http://localhost:5000/homologacao/api/status
Content-Type: application/json

{
  "status": "Em Análise" 
}
```

2. Resposta esperada:

* **202 Accepted**
* Proposta enviada para fila `proposta-fila`.
* `Saga.Orquestrador.Api` processa e define status (**Aprovada, Rejeitada, Pendente**).

---

## ✅ Resumo

* **Program.Api** → Porta de entrada (HTTP → RabbitMQ).
* **Saga.Orquestrador.Api** → Orquestrador (fila → regras → eventos).
* **Infraestructure.Api** → Persistência (SQL Server).
* **Docker Compose** → Sobe SQL Server + RabbitMQ.
* **Postman** → Testes rápidos dos endpoints.
