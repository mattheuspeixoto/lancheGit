using LanchesMac.Models;
using LanchesMac.Repository.Interface;
using LanchesMac.ViewsModels;
using Microsoft.AspNetCore.Authorization;
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

            // o Carrinho de compra é instanciado so com o ID, quando chega aqui é realizado uma consulta no banco para obter os itens do carrinho
            _carrinhocompra.CarrinhoCompraItems = _carrinhocompra.GetCarrinhoComprasItens();

            // A ViewModel Recebe a instancia do carrinho compra com o id do carrinho e os itens 
            var carrinhocompraVM = new CarrinhoCompraViewModels{
                vmCarrinhoCompra = _carrinhocompra,
                vmCarrinhoCompraTotal = _carrinhocompra.GetCarrinhoCompraTotal(),
            };
            return View(carrinhocompraVM);
        }
        [Authorize]
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId){
            var lancheselecionado = _lancheRepository.GetLanchebyID(lancheId);
            
            // Verifica se o lanche selecionado Existe
            if(lancheselecionado!=null){
                _carrinhocompra.AdicionarAoCarrinho(lancheselecionado);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public RedirectToActionResult RemoverItemNoCarrinhoCompra(int lancheId){
            var lancheselecionado = _lancheRepository.GetLanchebyID(lancheId);
            
            // Verifica se o lanche selecionado Existe
            if(lancheselecionado!=null){
                _carrinhocompra.RemoverDoCarrinho(lancheselecionado);
            }
            return RedirectToAction("Index");
        }

         public RedirectToActionResult Remover(int lancheId){
            var lancheselecionado = _lancheRepository.GetLanchebyID(lancheId);
            
            // Verifica se o lanche selecionado Existe
            if(lancheselecionado!=null){
                _carrinhocompra.Remover(lancheselecionado);
            }
            return RedirectToAction("Index");
        }
     
    }
}