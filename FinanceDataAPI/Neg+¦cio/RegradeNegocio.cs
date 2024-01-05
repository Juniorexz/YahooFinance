using FinanceDataAPI.ModeloDAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceDataAPI.Negócio
{
    public class RegradeNegocio
    {
        public string JsonDosAtivos()
        {
            // Criando instância
            FinanceData_DAO objDAO = new FinanceData_DAO();

            string[] nomes = objDAO.ConsultaNomesDosAtivos();
            var json = new { nomes };
            return JsonConvert.SerializeObject(json);
        }

        public string JsonDosValores(string Ativo)
        {
            // Criando instância
            FinanceData_DAO objDAO = new FinanceData_DAO();

            string valores = objDAO.ConsultaValoresDosUltimos30dias(Ativo);
            return valores;
        }

        public string JsonVariacaoRelativaAD1(string Ativo)
        {
            // Criando instância
            FinanceData_DAO objDAO = new FinanceData_DAO();

            string VariacaoRelativaAD1 = objDAO.CalcularVariacaoRelativaAD1(Ativo);
            return VariacaoRelativaAD1;
        }

        public string JsonVariacaoRelativaAoPrimeiroDia(string Ativo)
        {
            // Criando instância
            FinanceData_DAO objDAO = new FinanceData_DAO();

            string VariacaoRelativaAoPrimeiroDia = objDAO.CalcularVariacaoRelativaAoPrimeiroDia(Ativo);
            return VariacaoRelativaAoPrimeiroDia;
        }

        public string JsonFullReturn(string Ativo)
        {
            // Criando instância
            FinanceData_DAO objDAO = new FinanceData_DAO();

            string FullReturn = objDAO.FullReturn(Ativo);
            return FullReturn;
        }
    }
}
