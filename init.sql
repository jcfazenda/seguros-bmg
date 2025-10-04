-- Cria o banco
CREATE DATABASE homologacao;
GO

-- Usa o banco
USE homologacao;
GO

-- Cria tabela de exemplo
CREATE TABLE Proposta (
    Id UNIQUEIDENTIFIER PRIMARY KEY,   
    NomeCliente NVARCHAR(100) NOT NULL,
    Valor DECIMAL(18, 2) NOT NULL,   
    Status NVARCHAR(50) NOT NULL DEFAULT 'Em Análise',
    Cpf NVARCHAR(14) NOT NULL,  
    DataNascimento DATE NOT NULL
);
GO

/*  Rode o Container

    -- Parar e remover container antigo, se existir
    docker stop seguros_sqlserver
    docker rm seguros_sqlserver

    -- Rodar o container
    docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=proposta@56!' \
    -p 1433:1433 --name seguros_sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

    -- Verificar se está rodando 
    docker ps

    -- 
    Como vimos, o container não executa init.sql sozinho, então ele deve:
    Abrir Azure Data Studio (ou DBeaver / SSMS).
    Conectar ao SQL Server:

    Server: localhost
    Porta: 1433
    User: SA
    Password: proposta@56!
    Database: master


*/