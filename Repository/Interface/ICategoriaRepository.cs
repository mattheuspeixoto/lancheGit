using LanchesMac.Models;

namespace LanchesMac.Repository.Interface;
public interface ICategoriaRepository{
    public IEnumerable<Categoria> categoria  { get;}
}