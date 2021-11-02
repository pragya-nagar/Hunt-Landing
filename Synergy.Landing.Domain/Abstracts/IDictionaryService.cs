using System;
using System.Threading;
using System.Threading.Tasks;
using Synergy.Common.Domain.Models.Common;
using Synergy.Landing.Models;
using Synergy.Landing.Models.Dictionary;

namespace Synergy.Landing.Domain.Abstracts
{
    public interface IDictionaryService
    {
        Task<SearchResultModel<FastEntityModel<int>>> GetStateListAsync(SearchArgsModel args, CancellationToken cancellationToken = default);

        Task<SearchResultModel<UserModel>> GetUserListAsync(SearchArgsModel<UserFilterArgs, UserSortField> args, CancellationToken cancellationToken = default);

        Task<SearchResultModel<FastEntityModel<int>>> GetEnumDictionaryAsync<TEnum>(CancellationToken cancellationToken = default)
            where TEnum : Enum;

        Task<SearchResultModel<FastEntityModel<int>>> GetCountiesAsync(SearchArgsModel<CountySearchArgs, CountySortField> args, CancellationToken cancellationToken = default);

        Task<SearchResultModel<FastEntityModel<int>>> GetDepartmentsAsync(SearchArgsModel args, CancellationToken cancellationToken = default);
    }
}
