using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers {
    public class LancheController : Controller {

        private readonly ILanchesRepository _lancheRepository;

        public LancheController(ILanchesRepository lancheRepository) {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List() {
            ViewData["Titulo"] = "Todos os Lanches";
            ViewData["Data"] = DateTime.Now;
            var lanche = _lancheRepository.Lanches;
            var totallanches = lanche.Count();

            ViewBag.total = "Total de Lanches: ";
            ViewBag.totallanhces = totallanches;
            return View(lanche);
        }
    }
}
