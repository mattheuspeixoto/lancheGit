using LanchesMac.ViewsModels;
using LanchesMac.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers {
    public class LancheController : Controller {

        private readonly ILanchesRepository _lancheRepository;

        public LancheController(ILanchesRepository lancheRepository) {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List() {
            ViewData["Titulo"] = "Todos os Lanches";
            ViewData["Data"] = DateTime.Now.ToShortDateString();
            var lanche = _lancheRepository.Lanches;
            var totallanches = lanche.Count();

            ViewBag.total = "Total de Lanches: ";
            ViewBag.totallanhces = totallanches;

            // Puxa a lista de Lanches e passa para a View (Lanche/List.cshtml)
            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.lanches = _lancheRepository.Lanches;
            lancheListViewModel.Categoria = "Atual"; //Nao da pra chamar _lancheRepository.Lanches.Categoria pq Lanches é uma Lista
            return View(lancheListViewModel);
        }
    }
}
