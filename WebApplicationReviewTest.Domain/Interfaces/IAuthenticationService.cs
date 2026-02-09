namespace WebApplicationReviewTest.Domain.Interfaces;

using WebApplicationReviewTest.Domain.Entities;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(string username, string password);
    Task<User> GetAuthenticatedUserAsync(string username);
}
