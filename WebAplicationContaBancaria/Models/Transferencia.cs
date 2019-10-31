using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAplicationContaBancaria.Models
{
    public class Transferencia : Movimentacao
    {
        private double _taxaSobreOperacao { get { return 1.00; } }

        public Transferencia(Double valor, string destino)
        {
            if (destino == CPFProprietarioConta)
                throw new Exception("O destinatario não pode ser igual ao proprietário da conta corrente.");

            if (String.IsNullOrEmpty(destino))
                throw new Exception("Informe o destinatário corretamente.");

            if (valor <= 0)
                throw new Exception("Favor informar valor da operação corretamente.");

            TipoEvento = TipoMovimentacao.Transferencia;
            Data = DateTime.Now;
            Valor = valor;
            CPFDetinatario = destino;
        }

        public override double ObterTaxaDaOperacao()
        {
            return this._taxaSobreOperacao;
        }
        public override double CalcularCustoOperacao(double valor)
        {
            return Math.Round(this._taxaSobreOperacao, 2);
        }
    }
}