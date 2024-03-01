using LanchesMac.Models;

namespace LanchesMac.Repository.Interface;
public interface ILanchesRepository
{
    IEnumerable<Lanche> Lanches { get; }
    IEnumerable<Lanche> Lanchespreferidos { get; }
     Lanche GetLanchebyID(int lancheid);

}