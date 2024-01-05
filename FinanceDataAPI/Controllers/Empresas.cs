using FinanceDataAPI.Negócio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceDataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Empresas : ControllerBase
    {


        /// <summary>
        /// Retorna lista dos nomes dos ativos que estão disponíveis para consulta.
        /// </summary>
        /// <returns>string de sucesso</returns>
        /// <response code="200">Returna uma string de sucesso</response>
        [HttpGet("GetAtivos")]
        public string GetAtivos()
        {
            try
            {
                // Criando instância
                RegradeNegocio objDAO = new RegradeNegocio();

                string RetornoDosAtivos = objDAO.JsonDosAtivos();

                return RetornoDosAtivos;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
