using System.Linq.Expressions;
using Synergy.Landing.DAL.Queries.Entities;

namespace Synergy.Landing.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Synergy.Common.DAL.Abstract;
    using Synergy.Common.Domain.Models.Common;
    using Synergy.Landing.Domain.Abstracts;
    using Synergy.Landing.Models;
    using Synergy.Landing.Models.Dictionary;

    public class DictionaryService : IDictionaryService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private readonly IQueryProvider<DAL.Queries.Entities.User> _userQueryProvider;
        private readonly IQueryProvider<DAL.Queries.Entities.State> _stateQueryProvider;
        private readonly IQueryProvider<DAL.Queries.Entities.County> _countyQueryProvider;
        private readonly IQueryProvider<DAL.Queries.Entities.Department> _departmentQueryProvider;

        public DictionaryService(IMapper mapper,
            ILoggerFactory loggerFactory,
            IQueryProvider<DAL.Queries.Entities.User> userQueryProvider,
            IQueryProvider<DAL.Queries.Entities.State> stateQueryProvider,
            IQueryProvider<DAL.Queries.Entities.County> countyQueryProvider,
            IQueryProvider<DAL.Queries.Entities.Department> departmentQueryProvider)
        {
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = loggerFactory?.CreateLogger<DictionaryService>() ?? throw new ArgumentNullException(nameof(loggerFactory));

            this._userQueryProvider = userQueryProvider ?? throw new ArgumentNullException(nameof(userQueryProvider));
            this._stateQueryProvider = stateQueryProvider ?? throw new ArgumentNullException(nameof(stateQueryProvider));
            this._countyQueryProvider = countyQueryProvider ?? throw new ArgumentNullException(nameof(countyQueryProvider));
            this._departmentQueryProvider = departmentQueryProvider ?? throw new ArgumentNullException(nameof(departmentQueryProvider));
        }

        public async Task<SearchResultModel<FastEntityModel<int>>> GetStateListAsync(SearchArgsModel args, CancellationToken cancellationToken = default)
        {
            var query = this._stateQueryProvider.Query;

            if (string.IsNullOrWhiteSpace(args.FullSearch) == false)
            {
                var val = args.FullSearch.Trim();
                query = query.Where(x => x.Name.StartsWith(val) || x.Abbreviation.StartsWith(val));
            }

            var count = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(x => x.Name).Skip(args.Offset ?? 0).Take(args.Limit ?? 50);

            var list = await this._mapper.ProjectTo<FastEntityModel<int>>(query).ToListAsync(cancellationToken).ConfigureAwait(false);

            return new SearchResultModel<FastEntityModel<int>>
            {
                TotalCount = count,
                List = list,
            };
        }

        public async Task<SearchResultModel<UserModel>> GetUserListAsync(SearchArgsModel<UserFilterArgs, UserSortField> args, CancellationToken cancellationToken = default)
        {
            var query = this._userQueryProvider.Query;

            if (string.IsNullOrWhiteSpace(args.FullSearch) == false)
            {
                var parts = System.Text.RegularExpressions.Regex.Replace(args.FullSearch.Trim(), "\\s+", " ").Split(' ');
                query = parts.Aggregate(query, (current, part) => current.Where(x => x.FirstName.Contains(part) || x.LastName.Contains(part)));
            }

            query = query.Where(x => x.Id != new Guid("00000000-0000-0000-0000-000000000001"));

            if (args?.Filter?.DepartmentIds?.Any() == true)
            {
                var ids = args.Filter.DepartmentIds;
                query = query.Where(x => x.Roles.Any(y => y.Role.Departments.Any(z => ids.Contains(z.DepartmentId))));
            }

            var count = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            Expression<Func<User, object>> exp = null;
            switch (args?.SortField ?? UserSortField.Name)
            {
                case UserSortField.Name:
                    exp = x => x.FirstName + x.LastName;
                    break;
                case UserSortField.Department:
                    exp = x => x.Roles.Select(y => y.Role.Name).OrderBy(y => y).FirstOrDefault();
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid sort field");
            }

            query = (args?.SortOrder ?? SortOrder.Asc) == SortOrder.Asc ? query.OrderBy(exp) : query.OrderByDescending(exp);

            query = query.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Skip(args.Offset ?? 0).Take(args.Limit ?? 50);

            var list = await this._mapper.ProjectTo<UserModel>(query).ToListAsync(cancellationToken).ConfigureAwait(false);

            return new SearchResultModel<UserModel>
            {
                TotalCount = count,
                List = list,
            };
        }

        public async Task<SearchResultModel<FastEntityModel<int>>> GetEnumDictionaryAsync<TEnum>(CancellationToken cancellationToken = default)
            where TEnum : Enum
        {
            var items = this._mapper.Map<IEnumerable<FastEntityModel<int>>>(Enum.GetValues(typeof(TEnum)).Cast<Enum>());
            return await Task.FromResult(new SearchResultModel<FastEntityModel<int>>
            {
                TotalCount = items.Count(),
                List = items,
            }).ConfigureAwait(false);
        }

        public async Task<SearchResultModel<FastEntityModel<int>>> GetCountiesAsync(SearchArgsModel<CountySearchArgs, CountySortField> args, CancellationToken cancellationToken = default)
        {
            var query = this._countyQueryProvider.Query;

            if (string.IsNullOrWhiteSpace(args.FullSearch) == false)
            {
                query = query.Where(x => x.Name.ToLower().StartsWith(args.FullSearch.Trim().ToLower()));
            }

            var stateId = args?.Filter?.StateId;
            if (stateId.HasValue == true)
            {
                query = query.Where(x => x.StateId == stateId.Value);
            }

            query = query.OrderBy(x => x.Name).Skip(args.Offset ?? 0).Take(args.Limit ?? 50);
            var list = await this._mapper.ProjectTo<FastEntityModel<int>>(query).ToListAsync(cancellationToken).ConfigureAwait(false);

            var count = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            return new SearchResultModel<FastEntityModel<int>>
            {
                TotalCount = count,
                List = list,
            };
        }

        public async Task<SearchResultModel<FastEntityModel<int>>> GetDepartmentsAsync(SearchArgsModel args, CancellationToken cancellationToken = default)
        {
            var query = this._departmentQueryProvider.Query;

            if (string.IsNullOrWhiteSpace(args.FullSearch) == false)
            {
                var val = args.FullSearch.Trim();
                query = query.Where(x => x.Name.StartsWith(val) || x.Description.StartsWith(val));
            }

            var count = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(x => x.Name).Skip(args.Offset ?? 0).Take(args.Limit ?? 50);

            var list = await this._mapper.ProjectTo<FastEntityModel<int>>(query).ToListAsync(cancellationToken).ConfigureAwait(false);

            return new SearchResultModel<FastEntityModel<int>>
            {
                TotalCount = count,
                List = list,
            };
        }
    }
}
