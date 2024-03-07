using LanchesMac.Models;
using LanchesMac.ViewsModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components;

public class CarrinhoCompraResumo : ViewComponent
{
    private readonly CarrinhoCompra _carrinhocompra;
    public CarrinhoCompraResumo(CarrinhoCompra carrinhocompra)
    {
        _carrinhocompra = carrinhocompra;
    }
    public IViewComponentResult Invoke()
    {

        // o Carrinho de compra é instanciado so com o ID, quando chega aqui é realizado uma consulta no banco para obter os itens do carrinho
        _carrinhocompra.CarrinhoCompraItems = _carrinhocompra.GetCarrinhoComprasItens();

        // A ViewModel Recebe a instancia do carrinho compra
        var carrinhocompraVM = new CarrinhoCompraViewModels
        {
            vmCarrinhoCompra = _carrinhocompra,
            vmCarrinhoCompraTotal = _carrinhocompra.GetCarrinhoCompraTotal(),
        };
        return View(carrinhocompraVM);
    }
}