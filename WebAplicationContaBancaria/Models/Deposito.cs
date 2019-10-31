using System;

namespace WebAplicationContaBancaria.Models
{
    public class Deposito : Movimentacao
    {
        private double _taxaSobreOperacao { get { return 0.01;  } }

        public Deposito(Double valor)
        {
            if (valor <= 0)
                throw new Exception("Favor informar valor da operação corretamente.");

            TipoEvento = TipoMovimentacao.Deposito;
            Data = DateTime.Now;
            Valor = valor;
            CustoTaxaDeMovimentacao = this.CalcularCustoOperacao(this.Valor);
            CPFDetinatario = CPFProprietarioConta;
        }

        public override double ObterTaxaDaOperacao()
        {
            return this._taxaSobreOperacao;
        }
        public override double CalcularCustoOperacao(double valor)
        {
            return Math.Round(valor * this._taxaSobreOperacao, 2);
        }
        
    }
}