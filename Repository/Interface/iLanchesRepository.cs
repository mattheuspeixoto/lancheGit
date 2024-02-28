using LanchesMac.Models;

public interface iLanchesRespotory
{
    IEnumerable<Lanche> Lanches { get; }
    IEnumerable<Lanche> Lanchespreferidos { get; }
  Lanche GetLanchebyID(int lancheid);

}