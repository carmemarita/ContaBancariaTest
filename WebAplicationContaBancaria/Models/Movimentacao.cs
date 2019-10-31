using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAplicationContaBancaria.Models
{
    public abstract class Movimentacao
    {
        protected virtual DateTime Data { get; set; }
        public virtual Double Valor { get; set; }
        public virtual TipoMovimentacao TipoEvento { get; set; }
        public string Tipo { get { return this.TipoEvento.ToString(); } }
        public string DateEvento { get { return string.Concat(this.Data.ToShortDateString(), " ", this.Data.ToShortTimeString()) ; } }
        public string CPFDetinatario { get; set; }
        protected string CPFProprietarioConta { get { return "999.999.999-99";  } }
        public double CustoTaxaDeMovimentacao { get; set;  }
        public abstract double ObterTaxaDaOperacao();
        public abstract double CalcularCustoOperacao(double valor);
    }

    public enum TipoMovimentacao
    {
        Deposito,
        Saque,
        Transferencia,
        Extrato
    }

}
