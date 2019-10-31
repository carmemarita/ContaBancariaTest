using Microsoft.AspNetCore.Mvc;
using System;
using WebAplicationContaBancaria.Models;
using WebAplicationContaBancaria.Service;

namespace WebApplicationTest.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {

        [HttpGet]
        public Extrato GetExtrato()
        {
            return new MovimentacaoService().ObterExtrato();
        }

        [HttpPost("AddDeposito/{value}")]
        public void AddDeposito(string value)
        {
            try
            {
                var service = new MovimentacaoService();
                var deposito = new Deposito(this.ToDouble(value));
                service.AdicionarMovimentacao(deposito);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro. Favor executar a operação novamente.");
            }
        }

        [HttpPost("AddSaque/{value}")]
        public void Sacar(string value)
        {
            try
            {
                var service = new MovimentacaoService();
                var evento = new Saque(this.ToDouble(value));
                service.AdicionarMovimentacao(evento);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro. Favor executar a operação novamente.");
            } 
        }

        [HttpPost("AddTransferencia/")]
        public void AddTransferencia(string value, string cpfDestinatario)
        {
            try
            {
                var service = new MovimentacaoService();
                var evento = new Transferencia(this.ToDouble(value), cpfDestinatario);
                service.AdicionarMovimentacao(evento);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro. Favor executar a operação novamente.");
            }
        }

        private double ToDouble(string value)
        {
            double testeValor = 0.0;

            try
            {
                double.TryParse(value, out testeValor);
            }
            catch (Exception e)
            {
                //log de erro;
            }

            return testeValor;
        }
    }
}
