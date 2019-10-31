using NUnit.Framework;
using System;
using System.Linq;
using WebAplicationContaBancaria.Models;

namespace WebAplicationContaBancariaTest
{
    public class ExtratoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExtratoZerado()
        {
            var extrato = new Extrato(null);
            Assert.AreEqual((extrato.ListaOperacoes).ToList().Count(), 0);
        }

        [Test]
        public void ExtratoZeradoComSaldoZerado()
        {
            Assert.AreEqual((new Extrato(null)).Saldo, 0);
        }
    }
}