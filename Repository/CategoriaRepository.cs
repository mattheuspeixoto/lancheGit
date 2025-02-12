using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repository.Interface; 

namespace LanchesMac.Repository;
public class CategoriaRepository : ICategoriaRepository{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext contexto){
        _context = contexto;
    }
    public IEnumerable<Categoria> categoria => _context.Categorias;
}