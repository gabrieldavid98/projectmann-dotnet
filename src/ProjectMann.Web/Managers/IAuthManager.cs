using Microsoft.AspNetCore.Http;
using ProjectMann.Core.Domain;
using System.Threading.Tasks;

namespace ProjectMann.Web.Managers
{
    public interface IAuthManager
    {
        Task<Usuario> Validate(string email, string password);

        Task SignIn(HttpContext httpContext, Usuario user);

        Task SignOut(HttpContext httpContext);

        int GetCurrentUserId(HttpContext httpContext);
    }
}