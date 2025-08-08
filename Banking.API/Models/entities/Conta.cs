using Banking.API.Models.entities;
using Banking.API.Models.Enums;

namespace Banking.API.Models.Entities
{
    public class Conta
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!; // relação 1:N

        public string Agencia { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Moeda { get; set; } = "BRL";

        public decimal Saldo { get; private set; } = 0.0m;
        public StatusConta Status { get; private set; } = StatusConta.Ativa;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        public List<Movimento> Movimentos { get; set; } = new();

        // Métodos de domínio — regras básicas
        public void Depositar(decimal valor)
        {
            if (Status != StatusConta.Ativa)
                throw new InvalidOperationException("Conta não está ativa.");
            if (valor <= 0)
                throw new ArgumentOutOfRangeException(nameof(valor), "O valor do depósito deve ser positivo.");

            Saldo += valor;
            Movimentos.Add(Movimento.Credito(Id, valor, "Depósito"));
        }

        public void Sacar(decimal valor)
        {
            if (Status != StatusConta.Ativa)
                throw new InvalidOperationException("Conta não está ativa.");
            if (valor <= 0)
                throw new ArgumentOutOfRangeException(nameof(valor), "O valor do saque deve ser positivo.");
            if (Saldo < valor)
                throw new InvalidOperationException("Saldo insuficiente.");

            Saldo -= valor;
            Movimentos.Add(Movimento.Debito(Id, valor, "Saque"));
        }

        public void Bloquear() => Status = StatusConta.Bloqueada;

        public void Desbloquear() => Status = StatusConta.Ativa;

        public void Encerrar()
        {
            if (Saldo != 0)
                throw new InvalidOperationException("Só é possível encerrar contas com saldo zero.");
            Status = StatusConta.Encerrada;
        }
    }
}
