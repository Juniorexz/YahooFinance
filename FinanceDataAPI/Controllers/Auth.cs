using FinanceDataAPI.Entidades;
using FinanceDataAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceDataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Auth : ControllerBase
    {
        /// <summary>
        /// Retorna lista dos 30 últimos pregãos com a variação dos preços em relacão ao primeiro pregão.
        /// </summary>
        /// <returns>string de sucesso</returns>
        /// <response code="200">Retorna uma string de sucesso</response>
        [HttpPost("Auth")]
        public IActionResult auth(string username, string password)
        {
            if (username == "gdcampos" && password == "123456")
            {
                var token = TokenService.GenerateToken(new Employee());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}
