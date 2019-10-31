using System.Collections.Generic;
using WebAplicationContaBancaria.Models;

namespace WebAplicationContaBancaria.Repositorio
{
    public class MovimentacaoRepository
    {
        private List<Movimentacao> _lista;

        public MovimentacaoRepository()
        {
            if (_lista == null)
                _lista = this.InitMock();
        }

        public IEnumerable<Movimentacao> ObterMovimentacoes()
        {
            return _lista;
        }

        public void AdicionarMovimentacao(Movimentacao item)
        {
            _lista.Add(item);
        }

        public List<Movimentacao> InitMock()
        {
            return new List<Movimentacao>()
              {
                  new Deposito(300.2),
                  new Deposito(93.5),
                  new Deposito(2500.4),
                  new Deposito(21.3)
              };
        }
    }
}