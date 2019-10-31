using System;

namespace WebAplicationContaBancaria.Models
{
    public class Saque : Movimentacao
    {
        private double _taxaSobreOperacao { get { return 4.00; } }

        public Saque(Double valor)
        {
            if (valor <= 0)
                throw new Exception("Favor informar valor da operação corretamente.");

            TipoEvento = TipoMovimentacao.Saque;
            Data = DateTime.Now;
            Valor = valor;
            CPFDetinatario = CPFProprietarioConta;
        }

        public override double ObterTaxaDaOperacao()
        {
            return _taxaSobreOperacao;
        }
        public override double CalcularCustoOperacao(double valor)
        {
            return Math.Round(valor /this._taxaSobreOperacao, 2);
        }
    }
}