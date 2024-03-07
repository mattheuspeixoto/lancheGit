using LanchesMac.ViewsModels;
using LanchesMac.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using LanchesMac.Models;

namespace LanchesMac.Controllers {
    public class LancheController : Controller {

        private readonly ILanchesRepository _lancheRepository;

        public LancheController(ILanchesRepository lancheRepository) {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria) {
            ViewData["Data"] = DateTime.Now.ToShortDateString();
            var lanche = _lancheRepository.Lanches;
            var totallanches = lanche.Count();

            ViewBag.total = "Total de Lanches: ";
            ViewBag.totallanhces = totallanches;
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrWhiteSpace(categoria)){
                ViewData["Titulo"] = "Todos os Lanches";
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else{            
                ViewData["Titulo"] = categoria;
                lanches = _lancheRepository.Lanches
                .Where(l=> l.Categoria.CategoriaNome.Equals(categoria))
                .OrderBy(l=> l.Nome);
            }

            // Puxa a lista de Lanches e passa para a View (Lanche/List.cshtml)
            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.lanches = lanches;
            lancheListViewModel.Categoria = categoria; //Nao da pra chamar _lancheRepository.Lanches.Categoria pq Lanches é uma Lista
            return View(lancheListViewModel);
        }

        public IActionResult Details(int lancheId){
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l=> l.LancheId ==lancheId);
            return View(lanche);
        }
    }
}
