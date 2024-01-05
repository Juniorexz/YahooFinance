using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDataLoader.ModeloDAO
{
    public class YahooFinance_DAO
    {
        public void insertdata(DateTime date, double? value, string company)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=azureserverdamasceno.database.windows.net;Initial Catalog=Db_YahooFinance;User ID=azurelogon;Password=Palmeiras@1023"))
            {
                string query = "INSERT INTO T_HistoricalPrices (Date, Value, Company) VALUES (@Date, @Value, @Company)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date);
                    if (value.HasValue)
                    {
                        command.Parameters.AddWithValue("@Value", value.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Value", DBNull.Value);
                    }
                    command.Parameters.AddWithValue("@Company", company);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }

        public void TruncateTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = azureserverdamasceno.database.windows.net; Initial Catalog = Db_YahooFinance; User ID = azurelogon; Password = Palmeiras@1023"))
                {
                    connection.Open();

                    string commandText = $"TRUNCATE TABLE T_HistoricalPrices";

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao truncar tabela");
            }
        }

    }
}
