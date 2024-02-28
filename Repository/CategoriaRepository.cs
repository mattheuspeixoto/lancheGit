using LanchesMac.Context;
using LanchesMac.Models;

public class CategoriaRepository : iCategoriaRespotory{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext contexto){
        _context = contexto;
    }
    public IEnumerable<Categoria> categoria => _context.Categorias;
}