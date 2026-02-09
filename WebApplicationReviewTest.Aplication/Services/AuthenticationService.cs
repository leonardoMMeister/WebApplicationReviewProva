namespace WebApplicationReviewTest.Aplication.Services;

using WebApplicationReviewTest.Domain.Entities;
using WebApplicationReviewTest.Domain.Interfaces;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        
        if (user == null)
            return false;

        return user.Password == password;
    }

    public async Task<User> GetAuthenticatedUserAsync(string username)
    {
        return await _userRepository.GetByUsernameAsync(username);
    }
}
