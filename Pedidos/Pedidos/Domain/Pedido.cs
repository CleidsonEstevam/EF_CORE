using Pedidos.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public TipoFrete TipoFrete { get; set; }
        public StatusDoPedido Status { get; set; }
        public string Observacao { get; set; }
        public IReadOnlyCollection<PedidoItem> Items { get; set; }

    }
}
