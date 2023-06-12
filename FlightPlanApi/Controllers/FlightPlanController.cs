using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FlightPlanApi.Data;
using FlightPlanApi.Models;


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
        public async Task<IActionResult> FlightPlanList()
        { 
            var flightPlanList = await _database.GetAllFlightPlans();

            if (flightPlanList.Count == 0)
            {
                return NoContent(); //StatusCode(StatusCodes.Status204NoContent); 
            }

            return Ok(flightPlanList);
        }

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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateFlightPlan(FlightPlan flightPlan)
        {
            var tUpdResult = await _database.UpdateFlightPlan(flightPlan.FlightPlanId, flightPlan);

            switch (tUpdResult)
            {
                case TransactionResult.Success:
                    return Ok();

                case TransactionResult.NotFound:
                    return StatusCode(StatusCodes.Status404NotFound);

                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

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

        [HttpGet]
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

        [HttpGet]
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
