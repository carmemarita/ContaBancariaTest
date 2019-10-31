using System.Collections.Generic;
using System.Linq;

namespace WebAplicationContaBancaria.Models
{
    public class CalculadoraSaldo
    {
        private IEnumerable<Movimentacao> _lista;

        public CalculadoraSaldo(IEnumerable<Movimentacao> listaDeMovimentacoes)
        {
            if (listaDeMovimentacoes == null)
                listaDeMovimentacoes = new List<Movimentacao>();

            this._lista = listaDeMovimentacoes;
        }

        public double ObterSaldo()
        {
            var totalDepositos = CalcularTotalPorTipoComCustoOperacao(TipoMovimentacao.Deposito);
            var totalSaques = CalcularTotalPorTipoComCustoOperacao(TipoMovimentacao.Saque);
            var totalTransferencia = CalcularTotalPorTipoComCustoOperacao(TipoMovimentacao.Transferencia);

            return totalDepositos + totalSaques + totalTransferencia;
        }

        private double CalcularTotalPorTipo(TipoMovimentacao tipo)
        {
            return this._lista.Where(p => p.Tipo == tipo.ToString()).Sum(p => p.Valor);
        }

        private double CalcularTotalCustoPorTipo(TipoMovimentacao tipo)
        {
            return this._lista.Where(p => p.Tipo == tipo.ToString()).Sum(p => p.CustoTaxaDeMovimentacao);
        }

        private double CalcularTotalPorTipoComCustoOperacao(TipoMovimentacao tipo)
        {
            return ( ObterFatorPorTipoDeOperacao(tipo) * CalcularTotalPorTipo(tipo) ) + (-1 * CalcularTotalCustoPorTipo(tipo));
        }

        private int ObterFatorPorTipoDeOperacao(TipoMovimentacao tipo)
        {
            switch (tipo)
            {
                case (TipoMovimentacao.Deposito):
                    return 1;
                case (TipoMovimentacao.Saque):
                    return -1;
                case (TipoMovimentacao.Transferencia):
                    return -1;
                case (TipoMovimentacao.Extrato):
                    return 1;
            }
            return 1;
        }
    }
}