using Eateries.Application.Features.Eateries.Queries.GetEateries;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Infrastructure.Persistence.Repositories
{
    public class EateryRepositoryAsync : GenericRepositoryAsync<Eatery>, IEateryRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Eatery> _eatery;
        private readonly IDataShapeHelper<Eatery> _dataShaper;

        public EateryRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Eatery> dataShaper) : base(dbContext)
        {
            this._dbContext = dbContext;
            this._eatery = dbContext.Set<Eatery>();
            this._dataShaper = dataShaper;
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedEateriesReponseAsync(
            GetEateriesQuery requestParameter)
        {
            var eateryName = requestParameter.Name;
            var eateryType = requestParameter.EateryType;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _eatery
                .AsNoTracking();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, eateryName, eateryType);

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
                result = result.Select<Eatery>("new(" + fields + ")");
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
           ref IQueryable<Eatery> addresses,
           string eateryName,
           string eateryType)
        {
            if (!addresses.Any())
                return;

            if (string.IsNullOrEmpty(eateryType) && string.IsNullOrEmpty(eateryName))
                return;

            var predicate = PredicateBuilder.New<Eatery>();

            if (!string.IsNullOrEmpty(eateryName))
                predicate = predicate.Or(p => p.Name.Contains(eateryName.Trim()));

            if (!string.IsNullOrEmpty(eateryType))
            {
                predicate = predicate.Or(p => p.EateryType.ToString().Contains(eateryType.Trim()));
            }

            addresses = addresses.Where(predicate);
        }
    }
}
