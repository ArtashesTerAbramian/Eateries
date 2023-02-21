using System.Security.Cryptography;
using System.Text;
using Eateries.Application.Features.User.Queries.GetUsers;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using LinqKit;
using System.Linq.Dynamic.Core;

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

    public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedUsersReponseAsync(
        GetUsersQuery requestParameter)
    {
        var userFirstName = requestParameter.FirstName;
        var userLastName = requestParameter.LastName;
        var userEmail = requestParameter.Email;
        
        var pageNumber = requestParameter.PageNumber;
        var pageSize = requestParameter.PageSize;
        var orderBy = requestParameter.OrderBy;
        var fields = requestParameter.Fields;

        int recordsTotal, recordsFiltered;

        // Setup IQueryable
        var result = _user
            .AsNoTracking();

        // Count records total
        recordsTotal = await result.CountAsync();

        // filter data
        FilterByColumn(ref result, userFirstName, userLastName, userEmail);

        // Count records after filter
        recordsFiltered = await result.CountAsync();

        //set Record counts
        var recordsCount = new RecordsCount
        {
            RecordsFiltered = recordsFiltered,
            RecordsTotal = recordsTotal
        };

        // set order by
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            result = result.OrderBy(orderBy);
        }

        // select columns
        if (!string.IsNullOrWhiteSpace(fields))
        {
            result = result.Select<User>("new(" + fields + ")");
        }

        // paging
        result = result
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        // retrieve data to list
        var resultData = await result.ToListAsync();
        // shape data
        var shapeData = _dataShapeHelper.ShapeData(resultData, fields);

        return (shapeData, recordsCount);
    }

    private void FilterByColumn(ref IQueryable<User> users, string userFirstName, string userLastName, string userEmail)
        {
            if (!users.Any())
                return;

            if (string.IsNullOrEmpty(userLastName) && 
                string.IsNullOrEmpty(userFirstName) && 
                string.IsNullOrEmpty(userEmail))
                return;

            var predicate = PredicateBuilder.New<User>();

            if (!string.IsNullOrEmpty(userFirstName))
                predicate = predicate.Or(p => p.FirstName.Contains(userFirstName.Trim()));

            if (!string.IsNullOrEmpty(userLastName))
                predicate = predicate.Or(p => p.LastName.Contains(userLastName.Trim()));
            
            if (!string.IsNullOrEmpty(userEmail))
                predicate = predicate.Or(p => p.Email.Contains(userEmail.Trim()));

            users = users.Where(predicate);
        }
}