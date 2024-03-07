using LanchesMac.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components;

public class CategoriaMenu : ViewComponent{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaMenu(ICategoriaRepository categoriaRepository){
        _categoriaRepository = categoriaRepository;
    }

    public IViewComponentResult Invoke(){
        var categorias = _categoriaRepository.categoria.OrderBy(p => p.CategoriaNome);
        return View(categorias) ;  
    }
}