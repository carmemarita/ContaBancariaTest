using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebAplicationContaBancaria.Models;

namespace WebAplicationContaBancariaTest
{
    public class CalculadoraSaldoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalcularSaldoZerado()
        {
            Assert.AreEqual((new CalculadoraSaldo(null)).ObterSaldo(), 0);
        }

        [Test]
        public void CalcularSaldoComUmDeposito()
        {
            var deposito = new Deposito(100.50);
            var listaMovimentacoes = new List<Movimentacao>();
            listaMovimentacoes.Add(deposito);

            var calculadora = new CalculadoraSaldo(listaMovimentacoes);
            Assert.AreEqual(calculadora.ObterSaldo(), (deposito.Valor - deposito.CustoTaxaDeMovimentacao));
        }

        [Test]
        public void CalcularSaldoParaListaDeDepositos()
        {
            var numeroDeOperacoes = 2;
            var listaMovimentacoes = new List<Movimentacao>();
            var deposito = new Deposito(100.50);

            for (var i=0; i< numeroDeOperacoes; i++)
            {
                listaMovimentacoes.Add(deposito);
            }

            var calculadora = new CalculadoraSaldo(listaMovimentacoes);
            Assert.AreEqual(calculadora.ObterSaldo(), ((deposito.Valor - deposito.CustoTaxaDeMovimentacao)* numeroDeOperacoes));
        }

        [Test]
        public void CalcularSaldoComUmSaque()
        {
            var saque = new Saque(100.50);
            var listaMovimentacoes = new List<Movimentacao>();
            listaMovimentacoes.Add(saque);

            var calculadora = new CalculadoraSaldo(listaMovimentacoes);
            Assert.AreEqual(calculadora.ObterSaldo(), (-1*(saque.Valor + saque.CustoTaxaDeMovimentacao)));
        }

        [Test]
        public void CalcularSaldoParaListaDeSaques()
        {
            var numeroDeOperacoes = 2;
            var listaMovimentacoes = new List<Movimentacao>();
            var saque = new Saque(100.50);

            for (var i = 0; i < numeroDeOperacoes; i++)
            {
                listaMovimentacoes.Add(saque);
            }

            var calculadora = new CalculadoraSaldo(listaMovimentacoes);
            Assert.AreEqual(calculadora.ObterSaldo(), ((-1 * (saque.Valor + saque.CustoTaxaDeMovimentacao)) * numeroDeOperacoes));
        }

        [Test]
        public void CalcularSaldoComUmaTransferencia()
        {
            var transferencia = new Transferencia(100.50, "000.000.000-00");
            var listaMovimentacoes = new List<Movimentacao>();
            listaMovimentacoes.Add(transferencia);

            var calculadora = new CalculadoraSaldo(listaMovimentacoes);
            Assert.AreEqual(calculadora.ObterSaldo(), (-1 * (transferencia.Valor + transferencia.CustoTaxaDeMovimentacao)));
        }

        [Test]
        public void CalcularSaldoParaListaDeTransferencias()
        {
            var numeroDeOperacoes = 2;
            var listaMovimentacoes = new List<Movimentacao>();
            var transferencia = new Transferencia(100.50, "000.000.000-00");

            for (var i = 0; i < numeroDeOperacoes; i++)
            {
                listaMovimentacoes.Add(transferencia);
            }

            var calculadora = new CalculadoraSaldo(listaMovimentacoes);
            Assert.AreEqual(calculadora.ObterSaldo(), ((-1 * (transferencia.Valor + transferencia.CustoTaxaDeMovimentacao)) * numeroDeOperacoes));
        }

        [Test]
        public void CalcularSaldoDeMovimentacoes()
        {
            var listaMovimentacoes = new List<Movimentacao>();

            var deposito = new Deposito(500.50);
            listaMovimentacoes.Add(deposito);

            var saque = new Saque(10.82);
            listaMovimentacoes.Add(saque);

            var transferencia = new Transferencia(50.32, "000.000.000-00");
            listaMovimentacoes.Add(transferencia);

            var valorSaldoTeste =
                (deposito.Valor - deposito.CustoTaxaDeMovimentacao) +
                (-1 * (saque.Valor + saque.CustoTaxaDeMovimentacao)) +
                (-1 * (transferencia.Valor + transferencia.CustoTaxaDeMovimentacao));

            Assert.AreEqual(new CalculadoraSaldo(listaMovimentacoes).ObterSaldo(), valorSaldoTeste);
        }

        [Test]
        public void CalcularSaldoDeListaDeMovimentacoes()
        {
            var listaMovimentacoes = new List<Movimentacao>();
            var numeroDeDepositos = 5;
            var numeroDeSaques = 3;
            var numeroDeTransferencias = 4;

            var deposito = new Deposito(500.50);
            for (var i = 0; i < numeroDeDepositos; i++)
            {
                listaMovimentacoes.Add(deposito);
            }

            var saque = new Saque(10.82);
            for (var i = 0; i < numeroDeSaques; i++)
            {
                listaMovimentacoes.Add(saque);
            }

            var transferencia = new Transferencia(50.32, "000.000.000-00");
            for (var i = 0; i < numeroDeTransferencias; i++)
            {
                listaMovimentacoes.Add(transferencia);
            }

            var valorSaldoTeste =
                (deposito.Valor - deposito.CustoTaxaDeMovimentacao) * numeroDeDepositos +
                (-1 * (saque.Valor + saque.CustoTaxaDeMovimentacao)) * numeroDeSaques +
                (-1 * (transferencia.Valor + transferencia.CustoTaxaDeMovimentacao)) * numeroDeTransferencias;

            Assert.AreEqual(new CalculadoraSaldo(listaMovimentacoes).ObterSaldo(), valorSaldoTeste);
        }
    }
}