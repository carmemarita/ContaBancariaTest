using System;
using WebAplicationContaBancaria.Models;
using WebAplicationContaBancaria.Repositorio;

namespace WebAplicationContaBancaria.Service
{
    public class MovimentacaoService : IMovimentacaoRegras
    {
        private MovimentacaoRepository _repository;

        public MovimentacaoService()
        {
            if (_repository == null)
                _repository = new MovimentacaoRepository();
        }

        public Extrato ObterExtrato()
        {
            return new Extrato(_repository.ObterMovimentacoes());
        }

        public void AdicionarMovimentacao(Movimentacao item)
        {
            var registroValido  = this.RegistroValido(item);
            var temSaldo        = this.ValidarSaldo(item);

            if (temSaldo && registroValido)
                _repository.AdicionarMovimentacao(item);
            else
                throw new Exception("Transação não pode ser executada. Saldo indisponível");
        }

        private bool RegistroValido(Movimentacao item)
        {
            return (item != null && item.Valor > 0);
        }

        public bool ValidarSaldo(Movimentacao item)
        {
            if (!RegistroValido(item))
                throw new Exception("Operação inválida.");

            if (TipoMovimentacao.Saque == item.TipoEvento || TipoMovimentacao.Transferencia == item.TipoEvento)
            {
                var extrato = this.ObterExtrato();
                return extrato.Saldo + (-1 * item.Valor) >= 0;
            }

            return true;
        }
    }

    public interface IMovimentacaoRegras
    {
        bool ValidarSaldo(Movimentacao item);
    }
}