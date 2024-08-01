using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAware.API.Contracts;
using RouteAware.Application.Services;
using RouteAware.Core.Models;
using System.Net.Http.Headers;

namespace RouteAware.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccidentsController : ControllerBase
    {
        private readonly IAccidentsService accidentsService;
        private readonly HttpClient httpClient;

        public AccidentsController(IAccidentsService accidentsService, IHttpClientFactory httpClientFactory)  // docker-compose up
        {
            this.accidentsService = accidentsService;
            httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<ActionResult<List<AccidentsResponse>>> GetAccidents()
        {
            var accidents = await accidentsService.GetAllAccidents();

            var response = accidents.Select(a => new AccidentsResponse(a.Id, a.Title, a.Description, a.Region, a.Latitude, a.Longitude, a.ImageURL, a.CreationDateTime, a.DamageLevel));

            return Ok(response);
        }

        [HttpGet("{author:guid}")]
        public async Task<ActionResult<List<AccidentsResponse>>> GetAccidentsByAuthor(Guid author)
        {
            var accidents = await accidentsService.GetAccidentsByAuthor(author); 

            var response = accidents.Select(a => new AccidentsResponse(a.Id, a.Title, a.Description, a.Region, a.Latitude, a.Longitude, a.ImageURL, a.CreationDateTime, a.DamageLevel));

            return Ok(response);
        }

        [HttpGet("region/{region}")]
        public async Task<ActionResult<List<AccidentsResponse>>> GetAccidentsByRegion(string region)
        {
            var accidents = await accidentsService.GetAccidentsByRegion(region);

            var response = accidents.Select(a => new AccidentsResponse(a.Id, a.Title, a.Description, a.Region, a.Latitude, a.Longitude, a.ImageURL, a.CreationDateTime, a.DamageLevel));

            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> CreateAccident([FromBody] AccidentsRequest request)
        {            
            var (accident, error) = Accident.Create(
                Guid.NewGuid(), 
                request.Title, 
                request.Description,
                request.Region,
                request.Author,
                request.Latitude,
                request.Longitude,
                request.ImageURL,
                request.CreationDateTime,
                request.DamageLevel);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var accidentId = await accidentsService.CreateAccident(accident);

            return Ok(accidentId);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<Guid>> UpdateAccident(Guid id, [FromBody] AccidentsRequest request)
        {
            var accidentId = await accidentsService.UpdateAccident(
                id, 
                request.Title, 
                request.Description, 
                request.Region, 
                request.Latitude, 
                request.Longitude, 
                request.ImageURL, 
                request.CreationDateTime, 
                request.DamageLevel);

            return Ok(accidentId);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<Guid>> DeleteAccident(Guid id)
        {
            var accidentId = await accidentsService.DeleteAccident(id);

            return Ok(accidentId);
        }


        //////////////////////////
        [HttpPost("photo")]
        [Authorize]
        public async Task<ActionResult<PhotosResponse>> UploadAndModerateImage([FromForm] PhotosRequest request)
        {
            try
            {
                if (request == null || request.Image == null)
                {
                    return BadRequest("Invalid request. Image data missing.");
                }

                using var memoryStream = new MemoryStream();
                request.Image.CopyTo(memoryStream);

                bool isApproved = accidentsService.Moderate(memoryStream);

                if (isApproved)
                {
                    Guid photoGuid = Guid.NewGuid();
                    var uploadUrl = "http://localhost:9000/routeaware/"+photoGuid+".jpg";
                    
                    byte[] imageBytes = memoryStream.ToArray();

                    var content = new ByteArrayContent(imageBytes);

                    content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                    var response = await httpClient.PutAsync(uploadUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new PhotosResponse(uploadUrl));
                    }
                    else
                    {
                        // Обработка ошибки загрузки
                        return StatusCode((int)response.StatusCode, "Image upload failed.");
                    }
                }
                else
                {
                    //string defaultPhotoUrl = "http://localhost:9000/routeaware/carPhoto.jpg";
                    //return Ok(new PhotosResponse(defaultPhotoUrl));
                    return BadRequest("Image moderation failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /////
    }
}
