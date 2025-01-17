using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Repository.Interface; 

namespace LanchesMac.Repository;
public class LancheRepository : ILanchesRepository{
    private readonly AppDbContext _context;
    public LancheRepository(AppDbContext contexto){
        _context = contexto;
    }
    public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);
    public IEnumerable<Lanche> Lanchespreferidos => _context.Lanches.Where(c => c.IsLanchePreferido).Include(c => c.Categoria);
    public Lanche GetLanchebyID(int lancheid) => _context.Lanches.FirstOrDefault(l => l.LancheId == lancheid);
}