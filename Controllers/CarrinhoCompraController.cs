using LanchesMac.Models;
using LanchesMac.Repository.Interface;
using LanchesMac.ViewsModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers{
    public class CarrinhoCompraController : Controller{
        private readonly ILanchesRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhocompra;

        public CarrinhoCompraController(ILanchesRepository lancheRepository, CarrinhoCompra carrinhocompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhocompra = carrinhocompra;
        }

        public IActionResult Index(){
            _carrinhocompra.CarrinhoCompraItems = _carrinhocompra.GetCarrinhoComprasItens();

            // A ViewModel Recebe a instancia do carrinho compra
            var carrinhocompraVM = new CarrinhoCompraViewModels{
                vmCarrinhoCompra = _carrinhocompra,
                vmCarrinhoCompraTotal = _carrinhocompra.GetCarrinhoCompraTotal(),
            };
            return View(carrinhocompraVM);
        }
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId){
            var lancheselecionado = _lancheRepository.GetLanchebyID(lancheId);
            
            // Verifica se o lanche selecionado Existe
            if(lancheselecionado!=null){
                _carrinhocompra.AdicionarAoCarrinho(lancheselecionado);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoverItemNoCarrinhoCompra(int lancheId){
            var lancheselecionado = _lancheRepository.GetLanchebyID(lancheId);
            
            // Verifica se o lanche selecionado Existe
            if(lancheselecionado!=null){
                _carrinhocompra.RemoverDoCarrinho(lancheselecionado);
            }
            return RedirectToAction("Index");
        }
     
    }
}