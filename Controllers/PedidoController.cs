using LanchesMac.Models;
using LanchesMac.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class PedidoController : Controller{

    private readonly IPedidosRepository _pedidoRepository;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoController(IPedidosRepository pedidoRepository, CarrinhoCompra carrinhoCompra){
        _pedidoRepository = pedidoRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    [HttpGet]
    public IActionResult Checkout(){
        
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Pedido pedido){
        return View();
    }
}