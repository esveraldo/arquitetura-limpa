using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public IActionResult Get(string search, DateTime startDate, DateTime endDate/*, IOptions<PlacesConfiguration> options*/)
        {
            //var config = options.Value;
            //config.MinConfiguration.ToString();
            //config.MaxConfiguration.ToString();

            var result = _placeService.GetAllAvailable(search, startDate, endDate);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetById(int id)
        {
            var result = _placeService.GetById(id);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CreatePlaceInputModel model)
        {
            //throw new InvalidDataException();
            
            var result = _placeService.Insert(model);

            return CreatedAtAction(nameof(GetById), new { result.Data }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdatePlaceInputModel model)
        {
            var result = _placeService.Update(id, model);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{id}/amenities")]
        public IActionResult PostAmenity(int id, CreatePlaceAmenityInputModel model)
        {
            var result = _placeService.InsertAmenity(id, model);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _placeService.Delete(id);

            if (!result.IsSuccess)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpPost("{id}/books")]
        public IActionResult Post(int id, CreateBookInputModel model)
        {
            var result = _placeService.Book(id, model);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

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
