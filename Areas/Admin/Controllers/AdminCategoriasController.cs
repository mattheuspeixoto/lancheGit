using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;

namespace LanchesMac.Areas.Admin.Controllers {
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminCategoriasController : Controller {
        private readonly AppDbContext _context;

        public AdminCategoriasController(AppDbContext context) {
            _context = context;
        }

        // GET: Admin/AdminCategorias
        /*
        public async Task<IActionResult> Index() {
            return View(await _context.Categorias.ToListAsync());
        }
        */
         public async Task<IActionResult> Index(String filter, int pageindex = 1, string sort = "CategoriaNome")
        {
            var resultado = _context.Categorias.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.CategoriaNome.Contains(filter));
            }
            var userAgent = Request.Headers["User-Agent"].ToString();
            bool isMobileDevice = userAgent.Contains("Mobi");

            int pageSize = 10;
            if (isMobileDevice) {
                pageSize = 5; // Defina o número de registros por página para dispositivos móveis
            }
            var model = await PagingList.CreateAsync(resultado, pageSize, pageindex, "CategoriaNome", "CategoriaNome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminCategorias/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Categorias == null) {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (categoria == null) {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Admin/AdminCategorias/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Admin/AdminCategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaId,CategoriaNome,Descricao")] Categoria categoria) {
            if (ModelState.IsValid) {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Admin/AdminCategorias/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Categorias == null) {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Admin/AdminCategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaId,CategoriaNome,Descricao")] Categoria categoria) {
            if (id != categoria.CategoriaId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CategoriaExists(categoria.CategoriaId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Admin/AdminCategorias/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Categorias == null) {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (categoria == null) {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Admin/AdminCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Categorias == null) {
                return Problem("Entity set 'AppDbContext.Categorias'  is null.");
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null) {
                _context.Categorias.Remove(categoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id) {
            return _context.Categorias.Any(e => e.CategoriaId == id);
        }
    }
}
