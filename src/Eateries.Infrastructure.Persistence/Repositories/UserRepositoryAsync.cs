using System.Security.Cryptography;
using System.Text;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class UserRepositoryAsync : GenericRepositoryAsync<User>,IUserRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDataShapeHelper<User> _dataShapeHelper;
    private readonly ILogger<UserRepositoryAsync> _logger;
    private readonly DbSet<User> _user;

    public UserRepositoryAsync(
        ApplicationDbContext dbContext,
        IDataShapeHelper<User> dataShapeHelper,
        ILogger<UserRepositoryAsync> logger) : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _user = dbContext.Set<User>(); 
        _dataShapeHelper = dataShapeHelper;
        _logger = logger;
    }

    public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using (var hmac = new HMACSHA256())
        {
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password + passwordSalt)));
        }
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}