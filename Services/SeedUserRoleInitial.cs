using Microsoft.AspNetCore.Identity;

namespace LanchesMac.Services {
    public class SeedUserRoleInitial : ISeedUserRoleInitial {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public void SeedRoles() {
                                // Cria os Perfis caso nao existam
            if (!_roleManager.RoleExistsAsync("Member").Result) {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleresult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result) {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleresult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers() {

                // Cria dois usuários, (admin e membro) caso nao existam e os atribui aos perfis
            if(_userManager.FindByEmailAsync("usuario@localhost").Result == null) {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user,"Numsey#2022").Result;

                if(result.Succeeded) {
                    _userManager.AddToRoleAsync(user,"Member").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("admin@localhost").Result == null) {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded) {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
