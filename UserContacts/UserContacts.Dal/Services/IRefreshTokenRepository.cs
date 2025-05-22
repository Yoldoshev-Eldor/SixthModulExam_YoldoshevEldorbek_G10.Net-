using UserContacts.Core.Entities;

namespace UserContacts.Repository.Services;

public interface IRefreshTokenRepository
{
    Task AddRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken> SelectRefreshToken(string refreshToken, long userId);
}