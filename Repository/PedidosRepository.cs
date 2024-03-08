using LanchesMac.Context;
using LanchesMac.Models;

namespace LanchesMac.Repository;

public class PedidosRepository : IPedidosRepository{
    private readonly AppDbContext _context;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidosRepository(AppDbContext contexto, CarrinhoCompra carrinhoCompra){
        _context = contexto;
        _carrinhoCompra = carrinhoCompra;
    }

    public void CriarPedido(Pedido pedido){
        pedido.PedidoEnviado = DateTime.Now;
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItems;

        foreach (var carrinhoItem in carrinhoCompraItens){
            var PedidoDetalhe = new PedidoDetalhe(){
                Quantidade = carrinhoItem.Quantidade,
                LancheId = carrinhoItem.Lanche.LancheId,
                PedidoId = pedido.PedidoId,
                Preco = carrinhoItem.Lanche.Preco
            };
            _context.PedidoDetalhes.Add(PedidoDetalhe);
        }
        _context.SaveChanges();
    }
}