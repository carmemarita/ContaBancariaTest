using NUnit.Framework;
using System;
using WebAplicationContaBancaria.Models;

namespace WebAplicationContaBancariaTest
{
    public class DepositoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FazerOperacaoCorretamente()
        {
            var deposito = new Deposito(100);
            Assert.AreEqual(deposito.Valor, 100);
            Assert.AreEqual(deposito.TipoEvento, TipoMovimentacao.Deposito);
        }

        [Test]
        public void ObterTaxaOperacao()
        {
            Assert.AreEqual( new Deposito(100).ObterTaxaDaOperacao() , 0.01);
        }

        [Test]
        public void CalculaCustoOperacao()
        {
            var valorDepositado = 100.15;
            var taxaOperacaoDeposito    = new Deposito(valorDepositado).ObterTaxaDaOperacao();
            var testCustoCalculado      = new Deposito(valorDepositado).CalcularCustoOperacao(valorDepositado);

            Assert.AreEqual((Math.Round(valorDepositado * taxaOperacaoDeposito, 2)), testCustoCalculado);
        }

        [Test]
        public void MovimentacaoComValorZero()
        {
            var exception = Assert.Throws<Exception>( () => new Deposito(0) );
            Assert.AreEqual("Favor informar valor da operação corretamente.", exception.Message);
        }

        [Test]
        public void MovimentacaoComValorMenorZero()
        {
            var exception = Assert.Throws<Exception>(() => new Deposito(-100));
            Assert.AreEqual("Favor informar valor da operação corretamente.", exception.Message);
        }
    }
}