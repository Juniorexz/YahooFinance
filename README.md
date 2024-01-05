# Yahoo Finance API Data Collector

## Descrição
O Yahoo Finance API Data Collector é um projeto em C# .NET 5 que consulta a API do Yahoo Finance para obter os preços de ativos financeiros e armazena esses dados em um banco de dados Azure SQL. Ele consiste em uma aplicação console hospedada no Azure App Service, com um webjob agendado para atualizar a base de dados a cada hora, e uma API que consulta e retorna os dados armazenados.

Este projeto foi criado para facilitar a obtenção e o armazenamento de dados do mercado financeiro, permitindo a consulta dessas informações de forma eficiente e organizada.

## Tecnologias Utilizadas
- **C# .NET 5:** O C# é uma linguagem de programação orientada a objetos desenvolvida pela Microsoft. O .NET 5 é a última versão do framework .NET, que oferece suporte para o desenvolvimento de aplicativos modernos e escaláveis.
- **Azure SQL:** O Azure SQL é um serviço de banco de dados relacional totalmente gerenciado oferecido pela Microsoft na plataforma Azure. Ele fornece recursos de banco de dados SQL Server na nuvem, incluindo alta disponibilidade, segurança e escalabilidade.
- **Azure App Service:** O Azure App Service é um serviço de hospedagem de aplicativos web da Microsoft na plataforma Azure. Ele oferece suporte para várias linguagens de programação e frameworks, permitindo que os desenvolvedores publiquem e gerenciem aplicativos facilmente na nuvem.
- **Swagger:** Swagger é uma estrutura de código aberto que permite a criação, a documentação e a implementação de APIs REST de forma fácil e eficiente. Ele fornece uma interface interativa para que os desenvolvedores possam testar e entender melhor a API.
- **JWT (JSON Web Tokens):** JWT é um padrão aberto que define uma forma compacta e autossuficiente de transmitir informações de forma segura entre partes como um objeto JSON. Neste projeto, foi utilizado para autenticar e autorizar as requisições à API.

## Arquitetura do Projeto
O projeto é dividido em duas partes principais: a aplicação console e a API.

### Aplicação Console
A aplicação console é responsável por consultar a API do Yahoo Finance para obter os preços de ativos financeiros e armazenar esses dados no banco de dados Azure SQL. Ela é hospedada no Azure App Service e possui um webjob agendado para executar a atualização da base de dados a cada hora. A aplicação utiliza a biblioteca HttpClient para fazer requisições à API do Yahoo Finance e o Entity Framework Core para interagir com o banco de dados Azure SQL.

### API
A API é responsável por consultar o banco de dados Azure SQL e retornar os dados armazenados para o usuário. Ela foi implementada utilizando o Swagger para facilitar a documentação e o consumo da API. Os usuários podem fazer requisições HTTP para a API, especificando o ativo financeiro desejado, e a API retornará os dados correspondentes.

### Webjob
O webjob é uma função executável que é executada de forma contínua ou agendada em um ambiente do Azure App Service. No contexto deste projeto, o webjob é utilizado para agendar e executar a atualização da base de dados a cada hora, garantindo que os dados estejam sempre atualizados e prontos para serem consultados pela API.

### Comandos SQL e Funções
No banco de dados Azure SQL, foram executados os seguintes comandos SQL para criar as tabelas e funções necessárias para o funcionamento do projeto:

1. **Funções:**
   - `CREATE FUNCTION CalcularVariacaoRelativaAD1 ()
RETURNS TABLE
AS
RETURN
(
    SELECT
        [Id],
        [Date],
        [Value],
        [Company],
        CASE
            WHEN lag([Value]) OVER (ORDER BY [Date]) IS NULL THEN '-' -- Caso não haja um dia anterior, retorna '-'
            ELSE FORMAT((([Value] - lag([Value]) OVER (ORDER BY [Date])) / lag([Value]) OVER (ORDER BY [Date])) * 100, 'N2') + '%' -- Calcula a variação percentual em relação ao dia anterior
        END AS [Variacao em relacao a d1]
    FROM
    (
        SELECT TOP 30
            [Id],
            [Date],
            [Value],
            [Company]
        FROM
            [dbo].[T_HistoricalPrices]
        ORDER BY
            [Date] DESC
    ) AS Last30DaysData
);` :  Função que calcula a variação dos preços em relação ao pregão anterior

- `CREATE FUNCTION CalcularVariacaoRelativaAPrimeiraData ()
RETURNS TABLE
AS
RETURN
(
    SELECT
        [Id],
        [Date],
        [Value],
        [Company],
        CASE
            WHEN ROW_NUMBER() OVER (ORDER BY [Date] ASC) = 1 THEN '-' -- Caso seja o primeiro dia, retorna '-'
            ELSE FORMAT((([Value] - FIRST_VALUE([Value]) OVER (ORDER BY [Date] ASC)) / FIRST_VALUE([Value]) OVER (ORDER BY [Date] ASC)) * 100, 'N2') + '%' -- Calcula a variação percentual em relação ao primeiro dia
        END AS [Variacao em relacao ao primeiro dia]
    FROM
    (
        SELECT TOP 30
            [Id],
            [Date],
            [Value],
            [Company]
        FROM
            [dbo].[T_HistoricalPrices]
        ORDER BY
            [Date] DESC
    ) AS Last30DaysData
);`: Função que calcula a variação dos preços em relação ao primeiro pregão. 

 - `CREATE FUNCTION CombineVariacoesRelativas ()
RETURNS TABLE
AS
RETURN
(
    SELECT
        COALESCE(a.[Id], b.[Id]) AS [Id],
        a.[Date] AS [Date],
        a.[Value] AS [Value],
        a.[Company] AS [Company],
        a.[Variacao em relacao ao primeiro dia] AS [Variacao em relacao ao primeiro dia],
        b.[Variacao em relacao a d1] AS [Variacao em relacao a d1]
    FROM
        dbo.CalcularVariacaoRelativaAPrimeiraData() a
    FULL OUTER JOIN
        dbo.CalcularVariacaoRelativaAD1() b
    ON
        a.[Id] = b.[Id]
);`: Função que calcula a variação dos preços conforme solicitado no desafio.



2. **Tabelas:**
   - `CREATE TABLE [dbo].[T_HistoricalPrices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Value] [decimal](18, 14) NULL,
	[Company] [varchar](50) NULL,
 CONSTRAINT [PK__T_Histor__3214EC078DF8A1F8] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]`: Comando para criar a tabela utilizada no projeto.

## Instalação e Configuração
Para instalar e configurar o projeto, siga as instruções na documentação da microsoft. Basta a instalação do visual studio e .net 5. Ao compilar o projeto irá aparecer os pacotes que precisam ser instalados, o próprio visual studio permite a instalação dos pacotes via Nuget [Documentação Microsoft](https://learn.microsoft.com/pt-br/visualstudio/install/install-visual-studio?view=vs-2022).

## Uso
- **Consulta da API:**
   - A API pode ser consultada para obter os dados armazenados no banco de dados.
   - Requisições: As requisições podem ser visualizadas ao iniciar o projeto via swagger.

## Contribuição
Se você deseja contribuir com este projeto, por favor, siga os passos abaixo:
1. Faça um fork do repositório.
2. Crie uma branch para a sua feature (`git checkout -b feature/nova-feature`).
3. Faça commit das suas alterações (`git commit -am 'Adiciona nova feature'`).
4. Faça push para a branch (`git push origin feature/nova-feature`).
5. Crie um novo Pull Request.

## Licença
Este projeto é distribuído sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## Autores
- [Oswaldo Junior](https://github.com/Juniorexz)
