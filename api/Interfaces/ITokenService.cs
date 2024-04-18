using api.Models;

namespace api.Service;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
