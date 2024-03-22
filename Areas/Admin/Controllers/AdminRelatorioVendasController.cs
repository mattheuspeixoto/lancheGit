using LanchesMac.Areas.Admin.Servicos;
using LanchesMac.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RelatorioVendasService relatorioVendasService;

        public AdminRelatorioVendasController(RelatorioVendasService _relatorioVendasService, AppDbContext context)
        {
            relatorioVendasService = _relatorioVendasService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate, DateTime? maxDate, int pageindex = 1, string sort = "PedidoId")
        {
            //var resultado = from obj in _context.Pedidos select obj;
            string filter = "";

            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            var resultado = _context.Pedidos
                            .Include(l => l.PedidoItens)
                            .ThenInclude(l => l.Lanche)
                            .Where(p => p.PedidoEnviado >= minDate && p.PedidoEnviado <= maxDate)
                            .OrderByDescending(a => a.PedidoId)
                            .AsNoTracking().ToList();

            //var model = await PagingList.CreateAsync(resultado, 10, pageindex, "-PedidoId","PedidoId");
            // model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(resultado);
        }
    }
}
