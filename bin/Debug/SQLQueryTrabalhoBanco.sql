CREATE DATABASE AgenciaDallaRosa;
GO

USE AgenciaDallaRosa;
GO

CREATE TABLE Clientes (
    ID_Cliente INT IDENTITY(1,1),
    NomeCli VARCHAR(50) NOT NULL,
    CPF VARCHAR(14) NOT NULL,
    Altura INT NOT NULL,
    Contato NVARCHAR(30) NOT NULL,
    CONSTRAINT PK_Cliente PRIMARY KEY (ID_CLiente)
);

CREATE TABLE tb_user (
    Id_user INT IDENTITY(1,1),
    usuario NVARCHAR(50) NOT NULL,
    Senha NVARCHAR(40) NOT NULL,
    CONSTRAINT PK_tb_user PRIMARY KEY (Id_user)
);

CREATE TABLE tb_Veiculos (
    Id INT IDENTITY(1,1),
    Nome VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50) NOT NULL,
    Ano SMALLINT NOT NULL,
    Fabricacao SMALLINT NOT NULL,
    Cor NVARCHAR(20) NOT NULL,
    Combustivel TINYINT NOT NULL,
    Automatico BIT NOT NULL,
    Valor DECIMAL(18,2) NOT NULL,
    Situacao NVARCHAR(20) NOT NULL,
    CONSTRAINT PK_tb_Veiculos PRIMARY KEY (Id)
);

CREATE TABLE Vendas (
    Id_Venda INT IDENTITY(1,1),
    Id_Veiculo INT NOT NULL,
    Nome VARCHAR(50) NOT NULL,
    Modelo VARCHAR(14) NOT NULL,
    Ano SMALLINT NOT NULL,
    Fabricacao SMALLINT NOT NULL,
    Cor NVARCHAR(20) NOT NULL,
    Combustivel TINYINT NOT NULL,
    Automatico BIT NOT NULL,
    Valor DECIMAL(18,2) NOT NULL,
    ID_Cliente INT NOT NULL,
    NomeCli VARCHAR(50) NOT NULL,
    CPF VARCHAR(14) NOT NULL,
    Altura INT NOT NULL,
    Contato NVARCHAR(30) NOT NULL,
    DataVenda NVARCHAR(30) NOT NULL,
    CONSTRAINT PK_Vendas PRIMARY KEY (Id_Venda),
    CONSTRAINT FK_Vendas_Veiculo FOREIGN KEY (Id_Veiculo) REFERENCES tb_Veiculos(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Vendas_Cliente FOREIGN KEY (ID_Cliente) REFERENCES Clientes(ID_Cliente)
);
