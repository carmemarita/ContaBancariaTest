using NUnit.Framework;
using System;
using System.Linq;
using WebAplicationContaBancaria.Models;
using WebAplicationContaBancaria.Service;

namespace WebAplicationContaBancariaTest
{
    public class ServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ListarExtratoObtidosDoRepository()
        {
            var extrato = new MovimentacaoService().ObterExtrato();
            Assert.NotZero(extrato.ListaOperacoes.Count());
        }

        [Test]
        public void CalcularSaldoNoExtratoObtidosDoRepository()
        {
            var extrato = new MovimentacaoService().ObterExtrato();
            Assert.NotZero(extrato.Saldo);
        }

        [Test]
        public void DepositoApareceNoExtrato()
        {
            var service = new MovimentacaoService();
            var deposito = new Deposito(100.82);
            service.AdicionarMovimentacao(deposito);
            Assert.Contains(deposito, (service.ObterExtrato().ListaOperacoes).ToList());
        }

        [Test]
        public void DepositoSomadoNoSaldo()
        {
            var valor = 100.82;
            var service = new MovimentacaoService();
            var saldoAntes = service.ObterExtrato().Saldo;

            var deposito = new Deposito(valor);
            service.AdicionarMovimentacao(deposito);
            var saldoDepois = service.ObterExtrato().Saldo;

            Assert.AreEqual(Math.Round(saldoDepois,2), Math.Round((saldoAntes + deposito.Valor - deposito.CustoTaxaDeMovimentacao),2));
        }

        [Test]
        public void SaqueApareceNoExtrato()
        {
            var service = new MovimentacaoService();
            var movimentacao = new Saque(100.82);
            service.AdicionarMovimentacao(movimentacao);
            Assert.Contains(movimentacao, (service.ObterExtrato().ListaOperacoes).ToList());
        }

        [Test]
        public void SaqueSomadoNoSaldo()
        {
            var valor = 100.82;
            var service = new MovimentacaoService();
            var saldoAntes = service.ObterExtrato().Saldo;

            var movimentacao = new Saque(valor);
            service.AdicionarMovimentacao(movimentacao);
            var saldoDepois = service.ObterExtrato().Saldo;

            Assert.AreEqual(Math.Round(saldoDepois, 2), Math.Round((saldoAntes - (movimentacao.Valor + movimentacao.CustoTaxaDeMovimentacao)), 2));
        }

        [Test]
        public void TransferenciaApareceNoExtrato()
        {
            var service = new MovimentacaoService();
            var movimentacao = new Transferencia(100.82, "000.000.000-00");
            service.AdicionarMovimentacao(movimentacao);
            Assert.Contains(movimentacao, (service.ObterExtrato().ListaOperacoes).ToList());
        }

        [Test]
        public void TransferenciaSomadaNoSaldo()
        {
            var valor = 100.82;
            var service = new MovimentacaoService();
            var saldoAntes = service.ObterExtrato().Saldo;

            var movimentacao = new Transferencia(valor, "000.000.000-00"); ;
            service.AdicionarMovimentacao(movimentacao);
            var saldoDepois = service.ObterExtrato().Saldo;

            Assert.AreEqual(Math.Round(saldoDepois, 2), Math.Round((saldoAntes - (movimentacao.Valor + movimentacao.CustoTaxaDeMovimentacao)), 2));
        }

        [Test]
        public void DepositoComSaldo()
        {
            var service = new MovimentacaoService();
            Assert.IsTrue(service.ValidarSaldo(new Deposito(100)));
            Assert.IsTrue(service.ValidarSaldo(new Deposito(5000.50)));
        }

        [Test]
        public void SaqueComSaldo()
        {
            var service = new MovimentacaoService();
            Assert.IsTrue(service.ValidarSaldo(new Saque(100)));
        }

        [Test]
        public void SaqueSemSaldo()
        {
            var service = new MovimentacaoService();
            Assert.IsFalse(service.ValidarSaldo(new Saque(5000.50)));
        }

        [Test]
        public void TransferenciaComSaldo()
        {
            var service = new MovimentacaoService();
            Assert.IsTrue(service.ValidarSaldo(new Transferencia(100, "000.000.000-00")));
        }

        [Test]
        public void TransferenciaSemSaldo()
        {
            var service = new MovimentacaoService();
            Assert.IsFalse(service.ValidarSaldo(new Transferencia(5000.50, "000.000.000-00")));
        }

        [Test]
        public void MovimentacaoInvalida()
        {
            var service = new MovimentacaoService();
            var exception = Assert.Throws<Exception>(() => service.ValidarSaldo(null));
            Assert.AreEqual("Operação inválida.", exception.Message);
        }

    }
}