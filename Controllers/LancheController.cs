using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers {
    public class LancheController : Controller {

        private readonly ILanchesRepository _lancheRepository;

        public LancheController(ILanchesRepository lancheRepository) {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List() {
            var lanche = _lancheRepository.Lanches;
            return View(lanche);
        }
    }
}
