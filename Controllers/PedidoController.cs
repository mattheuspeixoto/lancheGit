using LanchesMac.Models;
using LanchesMac.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class PedidoController : Controller{

    private readonly IPedidosRepository _pedidoRepository;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoController(IPedidosRepository pedidoRepository, CarrinhoCompra carrinhoCompra){
        _pedidoRepository = pedidoRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Checkout(){
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Checkout(Pedido pedido){
        int totalItensPedido = 0;
        decimal precoTotalPedido = 0.0m;


        //Obtem os Itens do Carrinho de Compra do Cliente
        List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhoComprasItens();
        _carrinhoCompra.CarrinhoCompraItems = itens;

        //Verifica se Existem Itens de Pedido
        if (_carrinhoCompra.CarrinhoCompraItems.Count == 0){
            ModelState.AddModelError("", "Seu carrinho está vazio, que tal incluir um lanche ?");
        }

        //Calcula a Quantidade de Itens e o total do pedido
        foreach (var item in itens){
            totalItensPedido += item.Quantidade;
            precoTotalPedido = item.Quantidade * item.Lanche.Preco;
        }

        //Atribui os valores obtidos ao pedido
        pedido.TotalItensPedido = totalItensPedido;
        pedido.PedidoTotal = precoTotalPedido;


        //Valida os dados do pedido
        if (ModelState.IsValid){

          //Cria o pedido e os detalhes
          _pedidoRepository.CriarPedido(pedido);

          //Define Mensagens ao Cliente
          ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido";
          ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

          //Limpa o carrinho do Cliente
          _carrinhoCompra.LimparCarrinho();

          //Exibe a View com os dados do Cliente e do Pedido
          return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
        }
        return View (pedido);

    }
}