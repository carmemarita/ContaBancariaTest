using NUnit.Framework;
using System;
using WebAplicationContaBancaria.Models;

namespace WebAplicationContaBancariaTest
{
    public class TransferenciaTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FazerOperacaoCorretamente()
        {
            var movimentacao = new Transferencia(100, "000.000.000-00");
            Assert.AreEqual(movimentacao.Valor, 100);
            Assert.AreEqual(movimentacao.TipoEvento, TipoMovimentacao.Transferencia);
        }

        [Test]
        public void ObterTaxaOperacao()
        {
            Assert.AreEqual( new Transferencia(100, "000.000.000-00").ObterTaxaDaOperacao() , 1.00);
        }

        [Test]
        public void CalculaCustoOperacao()
        {
            var valor           = 100.15;
            var taxaOperacao    = 1.00;
            var custoCalculado  = new Transferencia(valor, "000.000.000-00").CalcularCustoOperacao(valor);

            Assert.AreEqual(taxaOperacao, custoCalculado);
        }

        [Test]
        public void MovimentacaoComValorZero()
        {
            var exception = Assert.Throws<Exception>( () => new Transferencia(0, "000.000.000-00"));
            Assert.AreEqual("Favor informar valor da operação corretamente.", exception.Message);
        }

        [Test]
        public void MovimentacaoComValorMenorZero()
        {
            var exception = Assert.Throws<Exception>(() => new Transferencia(-100, "000.000.000-00"));
            Assert.AreEqual("Favor informar valor da operação corretamente.", exception.Message);
        }

        [Test]
        public void TransferenciaComMesmoDestinatarioDaConta()
        {
            var exception = Assert.Throws<Exception>(() => new Transferencia(100, "999.999.999-99"));
            Assert.AreEqual("O destinatario não pode ser igual ao proprietário da conta corrente.", exception.Message);
        }

        [Test]
        public void TransferenciaComDestinatarioInvalido()
        {
            var exception = Assert.Throws<Exception>(() => new Transferencia(100, ""));
            Assert.AreEqual("Informe o destinatário corretamente.", exception.Message);
        }

        [Test]
        public void TransferenciaComDestinatarioVazio()
        {
            var exception = Assert.Throws<Exception>(() => new Transferencia(100, null));
            Assert.AreEqual("Informe o destinatário corretamente.", exception.Message);
        }

    }
}