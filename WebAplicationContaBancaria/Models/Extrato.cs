using System.Collections.Generic;

namespace WebAplicationContaBancaria.Models
{
    public class Extrato
    {
        public IEnumerable<Movimentacao> ListaOperacoes { get; set; }
        public double Saldo { get; set; }
        
        public Extrato(IEnumerable<Movimentacao> lista)
        {
            if (lista == null)
                lista = new List<Movimentacao>();

            this.ListaOperacoes = lista;
            this.Saldo = new CalculadoraSaldo(lista).ObterSaldo();
        }


    }

}
