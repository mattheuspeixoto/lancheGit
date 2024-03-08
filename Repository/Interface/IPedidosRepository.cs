using LanchesMac.Models;

namespace LanchesMac.Repository;

public interface IPedidosRepository{
    void CriarPedido(Pedido pedido);    
}