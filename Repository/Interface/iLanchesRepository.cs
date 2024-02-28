using LanchesMac.Models;

public interface ILanchesRepository
{
    IEnumerable<Lanche> Lanches { get; }
    IEnumerable<Lanche> Lanchespreferidos { get; }
     Lanche GetLanchebyID(int lancheid);

}