using FinanceDataAPI.Negócio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceDataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinanceData : ControllerBase
    {


        /// <summary>
        /// Retorna valor do ativo nos últimos 30 pregões.
        /// </summary>
        /// <returns>string de sucesso</returns>
        /// <response code="200">Retorna uma string de sucesso</response>
        [HttpGet("PriceHistory")]
        public string PriceHistory(string Ativo)
        {
            try
            {
                // Criando instância
                RegradeNegocio objDAO = new RegradeNegocio();

                string RetornoDosValores = objDAO.JsonDosValores(Ativo);

                return RetornoDosValores;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Retorna lista dos 30 últimos pregãos com a variação dos preços em relacão ao pregão anterior.
        /// </summary>
        /// <returns>string de sucesso</returns>
        /// <response code="200">Retorna uma string de sucesso</response>
        [HttpGet("VariacaoRelativaAD1")]
        public string VariacaoRelativaAD1(string Ativo)
        {
            try
            {
                // Criando instância
                RegradeNegocio objDAO = new RegradeNegocio();

                string RetornoDosValores = objDAO.JsonVariacaoRelativaAD1(Ativo);

                return RetornoDosValores;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Retorna lista dos 30 últimos pregãos com a variação dos preços em relacão ao primeiro pregão.
        /// </summary>
        /// <returns>string de sucesso</returns>
        /// <response code="200">Retorna uma string de sucesso</response>
        [HttpGet("VariacaoEmRelacaoAoPrimeiroDia")]
        public string VariacaoEmRelacaoAoPrimeiroDia(string Ativo)
        {
            try
            {
                // Criando instância
                RegradeNegocio objDAO = new RegradeNegocio();

                string RetornoDosValores = objDAO.JsonVariacaoRelativaAoPrimeiroDia(Ativo);

                return RetornoDosValores;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Retorna lista dos 30 últimos pregãos com a variação dos preços em relacão ao primeiro pregão e ao pregão anterior.
        /// </summary>
        /// <returns>string de sucesso</returns>
        /// <response code="200">Retorna uma string de sucesso</response>
        [Authorize]
        [HttpGet("fullreturn")]
        public string Retornocompleto(string Ativo)
        {
            try
            {
                // Criando instância
                RegradeNegocio objDAO = new RegradeNegocio();

                string RetornoDosValores = objDAO.JsonFullReturn(Ativo);

                return RetornoDosValores;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
