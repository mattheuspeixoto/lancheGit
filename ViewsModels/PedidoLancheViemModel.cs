using LanchesMac.Models;

namespace LanchesMac.ViewsModels;
    public class PedidoLancheViemModel {
        public Pedido pedido { get; set; }
        public IEnumerable<PedidoDetalhe> Pedidodetalhe { get; set; }
    }
