using LanchesMac.Models;

namespace LanchesMac.ViewsModels;

public class LancheListViewModel{
    public IEnumerable<Lanche> lanches {get;set;}
    public string Categoria {get;set;}
    }