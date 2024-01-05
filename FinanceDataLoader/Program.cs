using FinanceDataLoader.ModeloDAO;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinanceDataLoader
{
    class Program
    {
        // Instância estática e somente leitura da classe HttpClient
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            // Criando instância
            YahooFinance_DAO objDAO = new YahooFinance_DAO();

            // Limpa base de dados, para puxar dados atualizados a cada dia
            objDAO.TruncateTable();

            // Faz uma chamada assíncrona para obter os dados da API do Yahoo Finance para o ativo "PETR4.SA"
            var yahooFinanceData = await GetYahooFinanceData("VALE3.SA");

            // Verifica se Result[0] existe e se o array Timestamp não é nulo
            if (yahooFinanceData?.Chart?.Result?.Length > 0 && yahooFinanceData.Chart.Result[0].Timestamp != null)
            {
                string company = yahooFinanceData.Chart.Result[0].Meta.Symbol;

                // Percorrendo os dados da API para dar carga no banco de dados
                for (int i = 0; i < yahooFinanceData.Chart.Result[0].Timestamp.Length; i++)
                {
                    // Utilizando a classe DateTimeOffset para converter o timestamp UNIX em um objeto DateTime
                    DateTime date = DateTimeOffset.FromUnixTimeSeconds(yahooFinanceData.Chart.Result[0].Timestamp[i]).DateTime;
                    double? value = yahooFinanceData.Chart.Result[0].Indicators.Quote[0].Open[i];

                    // Salvando no banco de dados
                    objDAO.insertdata(date, value, company);

                }
            }


        }

        static async Task<YahooFinance> GetYahooFinanceData(string Company)
        {
            var url = $"https://query2.finance.yahoo.com/v8/finance/chart/{Company}";

            try
            {
                var response = await client.GetStringAsync(url);
                var yahooFinance = JsonConvert.DeserializeObject<YahooFinance>(response);
                return yahooFinance;
            }
            catch (HttpRequestException ex)
            {
                // Lidar com exceção de requisição HTTP
                Console.WriteLine($"Erro na requisição HTTP: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Lidar com exceção de desserialização JSON
                Console.WriteLine($"Erro na desserialização JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Lidar com outras exceções
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }

            return null; // Retornar null em caso de erro
        }


    }
}
