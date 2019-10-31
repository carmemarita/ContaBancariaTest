using NUnit.Framework;
using System;
using WebAplicationContaBancaria.Models;

namespace WebAplicationContaBancariaTest
{
    public class SaqueTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FazerOperacaoCorretamente()
        {
            var movimentacao = new Saque(100);
            Assert.AreEqual(movimentacao.Valor, 100);
            Assert.AreEqual(movimentacao.TipoEvento, TipoMovimentacao.Saque);
        }

        [Test]
        public void ObterTaxaOperacao()
        {
            Assert.AreEqual( new Saque(100).ObterTaxaDaOperacao() , 4.00);
        }

        [Test]
        public void CalculaCustoOperacao()
        {
            var valor           = 100.15;
            var taxaOperacao    = 4.00;
            var custoCalculado  = new Saque(valor).CalcularCustoOperacao(valor);

            Assert.AreEqual((Math.Round(valor/taxaOperacao, 2)), custoCalculado);
        }

        [Test]
        public void MovimentacaoComValorZero()
        {
            var exception = Assert.Throws<Exception>( () => new Saque(0) );
            Assert.AreEqual("Favor informar valor da operação corretamente.", exception.Message);
        }

        [Test]
        public void MovimentacaoComValorMenorZero()
        {
            var exception = Assert.Throws<Exception>(() => new Saque(-100));
            Assert.AreEqual("Favor informar valor da operação corretamente.", exception.Message);
        }
    }
}