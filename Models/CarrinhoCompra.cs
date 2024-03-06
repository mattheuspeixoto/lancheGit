using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models;

public class CarrinhoCompra
{
    private readonly AppDbContext _context;
    public CarrinhoCompra(AppDbContext contexto){
        _context = contexto;
    }
    //Carrinho de Compras Possui um ID e uma Lista de Itens
    public String CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

    public static CarrinhoCompra GetCarrinho(IServiceProvider services){
        //Define uma sessão
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        // Obtem um serviço do tipo do contexto
        var context = services.GetService<AppDbContext>();

        //Obtem ou gera o Id do carrinho
        string carrinhoid = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        //Atribui o id do carrinho na Sessão
        session.SetString("CarrinhoId", carrinhoid);

        //Retorna o carrinho com o contexto e o Id atribuido ou obtido
        return new CarrinhoCompra(context){
            CarrinhoCompraId = carrinhoid
        };
    }

    public void AdicionarAoCarrinho(Lanche lanche){

        // Verifica se o Carrinho e o Item Existem
        var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

        if (carrinhoCompraItem == null){
            carrinhoCompraItem = new CarrinhoCompraItem{
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.CarrinhoCompraItems.Add(carrinhoCompraItem);
        }
        else{
            carrinhoCompraItem.Quantidade++;
        }
        _context.SaveChanges();
    }

    public int RemoverDoCarrinho(Lanche lanche){
        var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault(
            s => s.Lanche.LancheId == lanche.LancheId &&
            s.CarrinhoCompraId == CarrinhoCompraId);
        var quantidadelocal = 0;
        if (carrinhoCompraItem != null){
            if (carrinhoCompraItem.Quantidade > 1){
                carrinhoCompraItem.Quantidade--;
                quantidadelocal = carrinhoCompraItem.Quantidade;
            }
            else{
                _context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
            }
        }
        _context.SaveChanges();
        return quantidadelocal;
    }

    public void Remover(Lanche lanche){
        var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault(
            s => s.Lanche.LancheId == lanche.LancheId &&
            s.CarrinhoCompraId == CarrinhoCompraId);
        if (carrinhoCompraItem != null){
            _context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
            _context.SaveChanges();
        }
    }
    public List<CarrinhoCompraItem> GetCarrinhoComprasItens(){
        return CarrinhoCompraItems ??
               (CarrinhoCompraItems =
                 _context.CarrinhoCompraItems.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(s => s.Lanche)
                .ToList());
    }

    public void LimparCarrinho(){
        var carrinhoItens = _context.CarrinhoCompraItems.Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
        _context.CarrinhoCompraItems.RemoveRange(carrinhoItens);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal(){
        var total = _context.CarrinhoCompraItems
        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
        .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
        return total;
    }
}


