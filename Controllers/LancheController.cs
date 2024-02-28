using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers {
    public class LancheController : Controller {

        private readonly iLanchesRespotory _lancheRepository;

        public LancheController(iLanchesRespotory lancheRepository) {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List() {
            var lanche = _lancheRepository.Lanches;
            return View(lanche);
        }
    }
}
