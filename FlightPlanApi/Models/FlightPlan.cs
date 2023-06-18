using System.Text.Json.Serialization;

namespace FlightPlanApi.Models
{
    /// <summary>
    /// Flight Plan model
    /// </summary>
    public class FlightPlan
    {
        /// <summary>
        /// Flight Plan Identifier
        /// </summary>
        [JsonPropertyName("flight_plan_id")]
        public string FlightPlanId { get; set; }

        /// <summary>
        /// Aircraft Identification
        /// </summary>
        [JsonPropertyName("aircraft_identification")]
        public string AircraftIdentification { get; set; }

        /// <summary>
        /// Aircraft Type
        /// </summary>
        [JsonPropertyName("aircraft_type")]
        public string AircraftType { get; set; }

        /// <summary>
        /// Aircraft average Air Speed
        /// </summary>
        [JsonPropertyName("airspeed")]
        public int Airspeed { get; set; }

        /// <summary>
        /// Aircraft average Altitude
        /// </summary>
        [JsonPropertyName("altitude")]
        public int Altitude { get; set; }

        /// <summary>
        /// Aircraft Flight Type
        /// </summary>
        [JsonPropertyName("flight_type")]
        public string FlightType { get; set; }

        /// <summary>
        /// Fuel Hours
        /// </summary>
        [JsonPropertyName("fuel_hours")]
        public int FuelHours { get; set; }

        /// <summary>
        /// Fuel Minutes
        /// </summary>
        [JsonPropertyName("fuel_minutes")]
        public int FuelMinutes { get; set; }

        /// <summary>
        /// Departure Time
        /// </summary>
        [JsonPropertyName("departure_time")]
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Estimated Arrival Time
        /// </summary>
        [JsonPropertyName("estimated_arrival_time")]
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Flight Departing Airport
        /// </summary>
        [JsonPropertyName("departing_airport")]
        public string DepartureAirport { get; set; }

        /// <summary>
        /// Flight Arrival Airport
        /// </summary>
        [JsonPropertyName("arrival_airport")]
        public string ArrivalAirport { get; set; }

        /// <summary>
        /// Flight Route
        /// </summary>
        [JsonPropertyName("route")]
        public string Route { get; set; }

        /// <summary>
        /// Flight Remarks
        /// </summary>
        [JsonPropertyName("remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// Aircraft Number On board
        /// </summary>
        [JsonPropertyName("number_onboard")]
        public int NumberOnBoard { get; set; }
    }
}
