using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinanceDataAPI.ModeloDAO
{
    public class FinanceData_DAO
    {
        public string[] ConsultaNomesDosAtivos()
        {
            string connectionString = "Server=tcp:azureserverdamasceno.database.windows.net,1433;Initial Catalog=Db_YahooFinance;Persist Security Info=False;User ID=azurelogon;Password=Palmeiras@1023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            string query = "SELECT DISTINCT Company FROM T_HistoricalPrices";

            List<string> nomes = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader["Company"].ToString();
                    nomes.Add(nome);
                }

                reader.Close();
            }

            return nomes.ToArray();
        }

        public string ConsultaValoresDosUltimos30dias(string Ativo)
        {
            try
            {
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

                using (SqlConnection connection = new SqlConnection("Server=tcp:azureserverdamasceno.database.windows.net,1433;Initial Catalog=Db_YahooFinance;Persist Security Info=False;User ID=azurelogon;Password=Palmeiras@1023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    string query = "SELECT TOP 30 [Id], [Date], [Value], [Company] FROM [dbo].[T_HistoricalPrices] WHERE Company = '"+ Ativo.Trim() + "' ORDER BY [Date] DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader.GetName(i), reader[i]);
                                }

                                results.Add(row);
                            }
                        }
                    }
                }

                return JsonSerializer.Serialize(results);
            }
            catch (Exception ex)
            {
                // Trate a exceção de acordo com a sua necessidade
                throw new Exception("Ocorreu um erro ao consultar os valores dos últimos 30 dias.", ex);
            }
        }

        public string CalcularVariacaoRelativaAD1(string Ativo) 
        {
            try
            {
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

                using (SqlConnection connection = new SqlConnection("Server=tcp:azureserverdamasceno.database.windows.net,1433;Initial Catalog=Db_YahooFinance;Persist Security Info=False;User ID=azurelogon;Password=Palmeiras@1023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    string query = "SELECT [Date],[Value],[Variacao em relacao a d1] as 'VariacaoEmRelacaoAoPregãoAnterior'  FROM CalcularVariacaoRelativaAD1() where Company = '" + Ativo.Trim() + "'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader.GetName(i), reader[i]);
                                }

                                results.Add(row);
                            }
                        }
                    }
                }

                return JsonSerializer.Serialize(results);
            }
            catch (Exception ex)
            {
                // Trate a exceção de acordo com a sua necessidade
                throw new Exception("Ocorreu um erro ao consultar os valores dos últimos 30 dias.", ex);
            }

        }

        public string CalcularVariacaoRelativaAoPrimeiroDia(string Ativo)
        {
            try
            {
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

                using (SqlConnection connection = new SqlConnection("Server=tcp:azureserverdamasceno.database.windows.net,1433;Initial Catalog=Db_YahooFinance;Persist Security Info=False;User ID=azurelogon;Password=Palmeiras@1023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    string query = "SELECT [Date],[Value],[Variacao em relacao ao primeiro dia] as 'VariacaoEmRelacaoAoPrimeiroPregão'  FROM CalcularVariacaoRelativaAPrimeiraData() where Company = '" + Ativo.Trim() + "'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader.GetName(i), reader[i]);
                                }

                                results.Add(row);
                            }
                        }
                    }
                }

                return JsonSerializer.Serialize(results);
            }
            catch (Exception ex)
            {
                // Trate a exceção de acordo com a sua necessidade
                throw new Exception("Ocorreu um erro ao consultar os valores dos últimos 30 dias.", ex);
            }

        }

        public string FullReturn(string Ativo)
        {
            try
            {
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

                using (SqlConnection connection = new SqlConnection("Server=tcp:azureserverdamasceno.database.windows.net,1433;Initial Catalog=Db_YahooFinance;Persist Security Info=False;User ID=azurelogon;Password=Palmeiras@1023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    string query = "SELECT [Id], [Date],[Value], [Company],[Variacao em relacao a d1] as 'VariacaoEmRelacaoAoPregãoAnterior', [Variacao em relacao ao primeiro dia] as 'VariacaoEmRelacaoAoPrimeiroPregão'  FROM CombineVariacoesRelativas() where Company = '" + Ativo.Trim() + "'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader.GetName(i), reader[i]);
                                }

                                results.Add(row);
                            }
                        }
                    }
                }

                return JsonSerializer.Serialize(results);
            }
            catch (Exception ex)
            {
                // Trate a exceção de acordo com a sua necessidade
                throw new Exception("Ocorreu um erro ao consultar os valores dos últimos 30 dias.", ex);
            }

        }
    }
}
