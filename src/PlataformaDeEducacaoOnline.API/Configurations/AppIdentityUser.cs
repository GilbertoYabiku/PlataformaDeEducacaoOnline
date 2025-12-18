using System.Security.Claims;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.API.Configurations
{
    public class AppIdentityUser(IHttpContextAccessor accessor) : IAppIdentityUser
    {
        public string GetUserId()
        {
            return accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        public bool IsAuthenticated()
        {
            return accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }
}
