using Eateries.Application.Features.Addresses.Queries.GetAddresses;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Repositories
{
    public class AddressRepositoryAsync : GenericRepositoryAsync<Address>, IAddressRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDataShapeHelper<Address> _dataShaper;
        private readonly ILogger<AddressRepositoryAsync> _logger;
        private readonly DbSet<Address> _address;

        public AddressRepositoryAsync(
            ApplicationDbContext dbContext,
            IDataShapeHelper<Address> dataShaper,
            ILogger<AddressRepositoryAsync> logger)
            : base(dbContext, logger)
        {
            this._dbContext = dbContext;
            this._address = dbContext.Set<Address>();
            this._dataShaper = dataShaper;
            _logger = logger;
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedAddressReponseAsync(
            GetAddressQuery requestParameter)
        {
            var addressStreet = requestParameter.Street;
            var addressCountry = requestParameter.Country;
            var addressCity = requestParameter.City;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _address
                .AsNoTracking();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, addressStreet, addressCountry, addressCity);

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
                result = result.Select<Address>("new(" + fields + ")");
            }

            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // retrieve data to list
            var resultData = await result.ToListAsync();
            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        private void FilterByColumn(
            ref IQueryable<Address> addresses,
            string addressNumber,
            string addressTitle,
            string addressCity)
        {
            if (!addresses.Any())
                return;

            if (string.IsNullOrEmpty(addressTitle) && string.IsNullOrEmpty(addressNumber))
                return;

            var predicate = PredicateBuilder.New<Address>();

            if (!string.IsNullOrEmpty(addressNumber))
                predicate = predicate.Or(p => p.Street.Contains(addressNumber.Trim()));

            if (!string.IsNullOrEmpty(addressTitle))
                predicate = predicate.Or(p => p.City.Contains(addressTitle.Trim()));

            if (!string.IsNullOrEmpty(addressCity))
                predicate = predicate.Or(p => p.City.Contains(addressCity.Trim()));

            addresses = addresses.Where(predicate);
        }
    }
}