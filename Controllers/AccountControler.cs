using LanchesMac.ViewsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class AccountController : Controller{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _siginManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> siginManager){
        _userManager = userManager;
        _siginManager = siginManager;
    }

    public IActionResult Login(string returnUrl){
        return View(new LoginViewModel(){
            returnURL = returnUrl
        });
    }

[HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM){
        
        // Testa se os campos foram preenchidos
        if (!ModelState.IsValid){
            return View(loginVM);
        }  
        
        //Busca o Usuario
        var user = await _userManager.FindByNameAsync(loginVM.UserName);

        if (user != null){
           
           //Tenta Fazer o Login com Usuario e Senha
            var result = await _siginManager.PasswordSignInAsync(user, loginVM.Password, false, false);
           

            if (result.Succeeded){

                // Testa se o Usuario Tentou acessar alguma pagina antes do login 
                if (string.IsNullOrWhiteSpace(loginVM.returnURL)){
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginVM.returnURL);
            }
        }
        ModelState.AddModelError("","Falha ao Realizar Login");
        return View(loginVM);  // Retorna para a Pagina de login
    }
}