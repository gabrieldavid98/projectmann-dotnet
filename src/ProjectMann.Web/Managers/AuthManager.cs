using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProjectMann.Core.Domain;
using ProjectMann.Infrastructure.Data;
using ProjectMann.Infrastructure.Crypto.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace ProjectMann.Web.Managers
{
   public class AuthManager : IAuthManager
   {
      private readonly ProjectMannDbContext _db;

      public AuthManager(ProjectMannDbContext db)
      {
         _db = db;
      }

      public int GetCurrentUserId(HttpContext httpContext)
      {
         if (!httpContext.User.Identity.IsAuthenticated) return -1;

         Claim claim = httpContext
            .User
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

         if (claim == null) return -1;

         if (!int.TryParse(claim.Value, out int currentUserId)) return -1;

         return currentUserId;
      }

      public Task SignIn(HttpContext httpContext, Usuario user)
      {
         var claims = new List<Claim>
         {
            new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
            new Claim(ClaimTypes.Name, $"{user.Nombre} {user.Apellido}".Trim()),
            new Claim(ClaimTypes.Role, user.FkRol.ToString()),
         };

         var identity = new ClaimsIdentity(
            claims, 
            authenticationType: CookieAuthenticationDefaults.AuthenticationScheme
         );

         var principal = new ClaimsPrincipal(identity);

         return httpContext.SignInAsync(
            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            properties: new AuthenticationProperties
            {
               IsPersistent = true,
            }
         );
      }

      public Task SignOut(HttpContext httpContext) =>
         httpContext.SignOutAsync(
            scheme: CookieAuthenticationDefaults.AuthenticationScheme
         );

      public Task<Usuario> Validate(string email, string password)
      {
         var encryptedPassword = password.Encrypt();

         return _db.Usuarios.FirstOrDefaultAsync(
             u => u.Email == email &&
             u.Clave == encryptedPassword
         );
      }
   }
}