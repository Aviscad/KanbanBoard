using System.Security.Claims;

namespace KanbanBoard.API.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user
                .Claims
                .SingleOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
        }
    }
}
