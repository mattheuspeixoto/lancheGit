using LanchesMac.Models;
using LanchesMac.Repository.Interface;
using LanchesMac.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LanchesMac.Controllers{
    public class HomeController : Controller{

        private readonly ILanchesRepository _lancheRepository;
        public HomeController(ILanchesRepository lanchesRepository)
        {
            _lancheRepository = lanchesRepository;
        }
        public IActionResult Index(){
            var HomeViewModel = new HomeViewModel{
                LanchesPreferidos = _lancheRepository.Lanchespreferidos
            };
            
            // Passa para a view (home/index.cstml) uma lista de lanches preferidos
            return View(HomeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}