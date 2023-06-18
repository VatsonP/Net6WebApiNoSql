using FlightPlanApi.Data;
using FlightPlanApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace FlightPlanApi.Controllers
{
    [Route("api/v1/flightplan")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private IDatabaseAdapter _database;

        public FlightPlanController(IDatabaseAdapter database)
        {
            _database = database;
        }

        [HttpGet]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "no Flight plans have been filed with this system")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized - the requested resource requires authentication")]
        public async Task<IActionResult> FlightPlanList()
        { 
            var flightPlanList = await _database.GetAllFlightPlans();

            if (flightPlanList.Count == 0)
            {
                return NoContent(); //StatusCode(StatusCodes.Status204NoContent); 
            }

            return Ok(flightPlanList);
        }

        /// <summary>
        /// Get Flight Plan data by Id from the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1/flightplan/4d2cbd544b1e487f80e832057ed9b2a1
        /// 
        /// </remarks>
        /// <param name="flightPlanId">The Identifier of the fight plan to be filed.</param>
        /// <response code="401">Unauthorized - the requested resource requires authentication</response>
        /// <response code="404">NotFound indicates that the requested resource does not exist on the server</response>
        /// <returns></returns> 
        [HttpGet]
        [Authorize]
        [Route("{flightPlanId}")]
        public async Task<IActionResult> GetFlightPlanById (string flightPlanId)
        {
            var flightPlanById = await _database.GetFlightPlanById(flightPlanId);

            if (flightPlanById.FlightPlanId != flightPlanId)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(flightPlanById);
        }

        /// <summary>
        /// Files a new flight plan with the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/flightplan/file
        ///     {
        ///         "aircraft_identification": "N67SVS",
        ///         "aircraft_type": "PA-34 Piper Seneca",
        ///         "airspeed": 128,
        ///         "altitude": 12000,
        ///         "flight_type": "VFR",
        ///         "fuel_hours": 3,
        ///         "fuel_minutes": 41,
        ///         "departure_time": "2022-07-08T00:26:45Z",
        ///         "estimated_arrival_time": "2022-07-08T03:49:45Z",
        ///         "departing_airport": "KBXA",
        ///         "arrival_airport": "KNZY",
        ///         "route": "KBXA JOH J46 DMDUP J46 KNZY",
        ///         "remarks": "",
        ///         "number_onboard": 4
        ///     }
        /// </remarks>
        /// <param name="flightPlan">The fight plan data to be filed.</param>
        /// <response code="400">BadRequest - There is a problem with the flight plan data received by this system</response>
        /// <response code="401">Unauthorized - the requested resource requires authentication</response>
        /// <response code="404">NotFound indicates that the requested resource does not exist on the server</response>
        /// <response code="500">InternalServerError - The flight plan is valid but this system cannot process it</response>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("file")]
        public async Task<IActionResult> FileFlightPlan(FlightPlan flightPlan)
        {
            var tInsResult = await _database.FileFlightPlan(flightPlan);

            switch (tInsResult)
            {
                case TransactionResult.Success:
                    return Ok();

                case TransactionResult.BadRequest:
                    return StatusCode(StatusCodes.Status400BadRequest);

                default:    
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update flight plan by Id into the system
        /// </summary>
        /// <param name="flightPlan">The fight plan data to be filed.</param>
        /// <response code="400">BadRequest - There is a problem with the flight plan data received by this system</response>
        /// <response code="401">Unauthorized - the requested resource requires authentication</response>
        /// <response code="404">NotFound indicates that the requested resource does not exist on the server</response>
        /// <response code="500">InternalServerError - The flight plan is valid but this system cannot process it</response>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateFlightPlan(FlightPlan flightPlan)
        {
            var tUpdResult = await _database.UpdateFlightPlan(flightPlan.FlightPlanId, flightPlan);

            switch (tUpdResult)
            {
                case TransactionResult.Success:
                    return Ok();

                case TransactionResult.BadRequest:
                    return StatusCode(StatusCodes.Status400BadRequest);

                case TransactionResult.NotFound:
                    return StatusCode(StatusCodes.Status404NotFound);

                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete flight plan by Id from the system
        /// </summary>
        /// <param name="flightPlanId">The Identifier of the fight plan to be filed.</param>
        /// <response code="401">Unauthorized - the requested resource requires authentication</response>
        /// <response code="404">NotFound indicates that the requested resource does not exist on the server</response>
        /// <returns></returns>        
        [HttpDelete]
        [Authorize]
        [Route("{flightPlanId}")]
        public async Task<IActionResult> DeleteFlightPlanById(string flightPlanId)
        {
            bool delResult = await _database.DeleteFlightPlanById(flightPlanId);

            if (delResult)
                return Ok();
            else
                return StatusCode(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Get flight plan Departure Airport by Id from the system
        /// </summary>
        /// <param name="flightPlanId">The Identifier of the fight plan to be filed.</param>
        /// <response code="401">Unauthorized - the requested resource requires authentication</response>
        /// <response code="404">NotFound indicates that the requested resource does not exist on the server</response>
        /// <returns></returns>  
        [HttpGet]
        [Authorize]
        [Route("airport/departure/{flightPlanId}")]
        public async Task<IActionResult> GetFlightPlanDepartureAirport(string flightPlanId)
        {
            var flightPlanById = await _database.GetFlightPlanById(flightPlanId);

            if (flightPlanById.FlightPlanId != flightPlanId)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(flightPlanById.DepartureAirport);
        }

        /// <summary>
        /// Get flight plan Route by Id from the system
        /// </summary>
        /// <param name="flightPlanId">The Identifier of the fight plan to be filed.</param>
        /// <response code="401">Unauthorized - the requested resource requires authentication</response>
        /// <response code="404">NotFound indicates that the requested resource does not exist on the server</response>
        /// <returns></returns>  
        [HttpGet]
        [Authorize]
        [Route("route/{flightPlanId}")]
        public async Task<IActionResult> GetFlightPlanRoute(string flightPlanId)
        {
            var flightPlanById = await _database.GetFlightPlanById(flightPlanId);

            if (flightPlanById.FlightPlanId != flightPlanId)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(flightPlanById.Route);
        }

        /// <summary>
        /// Get flight plan Time Enroute by Id from the system
        /// </summary>
        /// <param name="flightPlanId">The Identifier of the fight plan to be filed.</param>
        /// <response code="401">Unauthorized - the requested resource requires authentication</response>
        /// <response code="404">NotFound indicates that the requested resource does not exist on the server</response>
        /// <returns></returns>  
        [HttpGet]
        [Authorize]
        [Route("time/enroute/{flightPlanId}")]
        public async Task<IActionResult> GetFlightPlanTimeEnroute(string flightPlanId)
        {
            var flightPlanById = await _database.GetFlightPlanById(flightPlanId);

            if (flightPlanById.FlightPlanId != flightPlanId)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var estimatedTimeEnroute = flightPlanById.ArrivalTime - flightPlanById.DepartureTime;

            return Ok(estimatedTimeEnroute);
        }

    }
}
