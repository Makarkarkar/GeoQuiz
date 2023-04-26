using GeoQuiz.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GeoQuiz.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LandmarksController:ControllerBase
{
    private readonly ILandmarkService _service;
    
    public LandmarksController(ILandmarkService service)
    {
        _service = service;
    }
    [ProducesResponseType(404)]
    [HttpGet("{num}")]
    [EnableCors] 
    public IEnumerable<Landmark> GetRandomLandmarks(int num)
    {
        return _service.GetRandomLandmarks(num);
    }
    [ProducesResponseType(404)]
    [HttpGet("{id}")]
    [EnableCors] 
    public Landmark GetById(int id)
    {
        return _service.GetById(id);
    }
}