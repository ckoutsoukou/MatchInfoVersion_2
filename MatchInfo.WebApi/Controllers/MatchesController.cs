using MatchInfo.WebApi.Filters;
using MatchInfo.WebApi.Models;
using MatchInfo.API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using MatchInfo.WebApi.ServicesAbstractions;

namespace MatchInfo.WebApi.Controllers
{
    /// <summary>
    /// A class for MatchesController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController: ControllerBase
    {

        private readonly IServiceManager _serviceManager;
        /// <summary>
        /// Ctor for MatchesController.
        /// </summary>
        /// <param name="uow"></param>
        public MatchesController(IServiceManager serviceManager) => _serviceManager = serviceManager;
  

        /// <summary>
        /// Get all matches.
        /// </summary>
        /// <param name="includeMatchOdds">Optionally request that records include match odds for each match.</param>
        /// <returns>A list of matches</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public ActionResult<IEnumerable<MatchDto>> GetAll(string? includeProperties = null)
        {
            var matchesDtos = this._serviceManager.MatchService.GetAll(asNoTracking: true, includeProperties: includeProperties);
            return Ok(matchesDtos);
        }

        /// <summary>
        /// Get match by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeMatchOdds">Optionally request that records include match odds for each match.</param>
        /// <returns>A match</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id}")]
        public ActionResult<MatchDto> GetById([Required] int id,string? includeProperties = null)
        {
            var matchDto = this._serviceManager.MatchService.GetById(id:id, asNoTracking: true, includeProperties: includeProperties);
            return Ok(matchDto);
        }

        /// <summary>
        /// Inserts a match.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>An ActionResult of type MatchDto.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public ActionResult<MatchDto> Post([Required][FromBody] MatchDto model)
        {          
            var insertedItem = this._serviceManager.MatchService.Insert(model: model);



            return Ok(insertedItem);
        }

        /// <summary>
        /// Updates an existing match.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TAn ActionResult of type MatchDto.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public ActionResult<MatchDto> Put([Required][FromBody] MatchDto model)
        {
            var updatedItem = this._serviceManager.MatchService.Update( model);
            return Ok(updatedItem);
        }

        /// <summary>
        /// Deletes a match.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The action result.</returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id}")]
        public ActionResult Delete([Required] int id)
        {
            this._serviceManager.MatchService.Delete(id: id);
            return NoContent();
        }
    }
}
