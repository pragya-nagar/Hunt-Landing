using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Synergy.Common.Domain.Models.Common;
using Synergy.Landing.Domain.Abstracts;
using Synergy.Landing.Models;
using Synergy.Landing.Models.Dictionary;

namespace Synergy.Landing.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public class DictionariesController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDictionaryService _dictionaryService;

        public DictionariesController(ILoggerFactory loggerFactory, IDictionaryService dictionaryService)
        {
            this._logger = loggerFactory?.CreateLogger<DictionariesController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            this._dictionaryService = dictionaryService ?? throw new ArgumentNullException(nameof(dictionaryService));
        }

        [Route("States")]
        [HttpGet]
        [ProducesResponseType(typeof(SearchResultModel<FastEntityModel<int>>), 200)]
        public async Task<IActionResult> GetStates([FromQuery]SearchArgsModel args, CancellationToken cancellationToken = default)
        {
            var result = await this._dictionaryService.GetStateListAsync(args, cancellationToken).ConfigureAwait(false);
            return this.Ok(result);
        }

        [Route("Users")]
        [HttpGet]
        [ProducesResponseType(typeof(SearchResultModel<UserModel>), 200)]
        public async Task<IActionResult> GetUsers([FromQuery]SearchArgsModel<UserFilterArgs, UserSortField> args, CancellationToken cancellationToken = default)
        {
            var result = await this._dictionaryService.GetUserListAsync(args, cancellationToken).ConfigureAwait(false);
            return this.Ok(result);
        }

        [Route("TaskTypes")]
        [HttpGet]
        [ProducesResponseType(typeof(SearchResultModel<FastEntityModel<int>>), 200)]
        public async Task<IActionResult> GetTaskTypes(CancellationToken cancellationToken = default)
        {
            var res = await this._dictionaryService.GetEnumDictionaryAsync<Models.TaskType>(cancellationToken).ConfigureAwait(false);
            return this.Ok(res);
        }

        [Route("TaskStatus")]
        [HttpGet]
        [ProducesResponseType(typeof(SearchResultModel<FastEntityModel<int>>), 200)]
        public async Task<IActionResult> GetTaskStatuses(CancellationToken cancellationToken = default)
        {
            var res = await this._dictionaryService.GetEnumDictionaryAsync<Models.TaskStatus>(cancellationToken).ConfigureAwait(false);
            return this.Ok(res);
        }

        [Route("EventTypes")]
        [HttpGet]
        [ProducesResponseType(typeof(SearchResultModel<FastEntityModel<int>>), 200)]
        public async Task<IActionResult> GetEventTypes(CancellationToken cancellationToken = default)
        {
            var res = await this._dictionaryService.GetEnumDictionaryAsync<Models.EventType>(cancellationToken).ConfigureAwait(false);
            return this.Ok(res);
        }

        [Route("Counties")]
        [HttpGet]
        [ProducesResponseType(typeof(SearchResultModel<FastEntityModel<int>>), 200)]
        public async Task<IActionResult> GetCounties([FromQuery]SearchArgsModel<CountySearchArgs, CountySortField> args, CancellationToken cancellationToken = default)
        {
            var res = await this._dictionaryService.GetCountiesAsync(args, cancellationToken).ConfigureAwait(false);
            return this.Ok(res);
        }

        [Route("departments")]
        [HttpGet]
        [ProducesResponseType(typeof(SearchResultModel<FastEntityModel<int>>), 200)]
        public async Task<IActionResult> GetDepartments([FromQuery]SearchArgsModel args, CancellationToken cancellationToken = default)
        {
            var res = await this._dictionaryService.GetDepartmentsAsync(args, cancellationToken).ConfigureAwait(false);
            return this.Ok(res);
        }
    }
}
