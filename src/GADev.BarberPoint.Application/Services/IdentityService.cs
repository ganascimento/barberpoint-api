using Microsoft.AspNetCore.Http;

namespace GADev.BarberPoint.Application.Services.Interfaces
{
    public class IdentityService : Interfaces.IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserEmail()
        {
            return _context.HttpContext.User.FindFirst("email").Value;
        }

        public int GetUserIdentity()
        {
            return int.Parse(_context.HttpContext.User.FindFirst("userId").Value);
        }
    }
}