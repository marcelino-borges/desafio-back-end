using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using System.Text.Json;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class TemperaturePlaylistController : ControllerBase
    {
        [HttpGet]
        [HttpGet("{city}")]
        public async Task<IActionResult> GetPlaylistByCity(string city)
        {
            try
            {
                string jsonResult = JsonSerializer.Serialize(await ApiController.GetPlaylistByCity(city));
                return Ok(jsonResult);
            } catch(Exception e)
            {
                throw e;
            }
            
        }

        [HttpGet]
        [HttpGet("lat={lat}&lon={lon}")]
        public async Task<IActionResult> GetPlaylistByCoordinate(string lat, string lon)
        {
            try
            {
                //coordenadas recife: -8.063021, -34.871342
                Coordinate coordinates = new Coordinate(lat, lon);
                string jsonResult = JsonSerializer.Serialize(await ApiController.GetPlaylistByCoordinate(coordinates));
                return Ok(jsonResult);
            } catch(Exception e)
            {
                throw e;
            }
        }
    }
}
