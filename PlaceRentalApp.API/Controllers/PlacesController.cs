using Microsoft.AspNetCore.Mvc;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Application.Services;
using PlaceRentalApp.Core.Entities;
using PlaceRentalApp.Infraestructure.Persistence;

namespace PlaceRentalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly PlaceRentalDbContext _context;
        private readonly IPlaceService _placeService;

        public PlacesController(PlaceRentalDbContext context, IPlaceService placeService)
        {
            _context = context;
            _placeService = placeService;
        }

        [HttpGet]
        public IActionResult Get(string search, DateTime startDate, DateTime endDate/*, IOptions<PlacesConfiguration> options*/)
        {
            //var config = options.Value;
            //config.MinConfiguration.ToString();
            //config.MaxConfiguration.ToString();

            var availablePlaces = _placeService.GetAllAvailable(search, startDate, endDate);

            return Ok(availablePlaces);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var place = _placeService.GetById(id);  
            return Ok(place);
        }

        [HttpPost]
        public IActionResult Post(CreatePlaceInputModel model)
        {
            //throw new InvalidDataException();
            
            var id = _placeService.Insert(model);

            return CreatedAtAction(nameof(GetById), new { id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdatePlaceInputModel model)
        {
            _placeService.Update(id, model);

            return NoContent();
        }

        [HttpPost("{id}/amenities")]
        public IActionResult PostAmenity(int id, CreatePlaceAmenityInputModel model)
        {
            _placeService.InsertAmenity(id, model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _placeService.Delete(id);   
            
            return NoContent();
        }

        [HttpPost("{id}/books")]
        public IActionResult Post(int id, CreateBookInputModel model)
        {
            _placeService.Book(id, model);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateCommentInputModel model)
        {
            
            return NoContent();
        }

        [HttpPut("{id}/photos")]
        public IActionResult PostPlacePhoto(int id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var base64 = Convert.ToBase64String(fileBytes);

                return Ok(new { description, base64 });
            }
        }
    }
}
