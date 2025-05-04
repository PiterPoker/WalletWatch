using AuthWalletWatch.Infrastructure.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace AuthWalletWatch.Infrastructure.NoSQL;

public class RedisTokenRepository : ITokenRepository
{
    private readonly IDatabase _database;

    public RedisTokenRepository(IDatabase database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
    }

    public void Create(ApplicationUserToken token)
    {
        string key = token.UserId.ToString();

        var tokenDto = new AccessTokenDto()
        {
            AccessToken = token.Value,
            TokenType = token.LoginProvider,
            ExpiresIn = token.Create,
        };

        string value = JsonSerializer.Serialize(tokenDto);
        _database.StringSet(key, value, TimeSpan.FromSeconds(token.ExpiresAt));
    }

    public ApplicationUserToken? Get(Guid userId)
    {
        string key = userId.ToString();
        string value = _database.StringGet(key);


        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        var tokenDto = JsonSerializer.Deserialize<AccessTokenDto>(value);

        return new ApplicationUserToken 
        {
            Create = tokenDto.ExpiresIn,
            Value = tokenDto.AccessToken,
            LoginProvider = tokenDto.TokenType,
            UserId = userId,
        };
    }

    public void Delete(Guid userId)
    {
        string key = userId.ToString();
        _database.KeyDelete(key);
    }
}
