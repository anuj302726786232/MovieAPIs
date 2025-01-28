using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.MovieContext;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private MoviDbContext _dbContext;

        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger, MoviDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("allmovie")]
        public IActionResult GetMovie()
        {
            return Ok(_dbContext.Movies.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] long id)
        {
            return Ok(_dbContext.Movies.FirstOrDefault(movie => movie.Id == id));
        }

        [HttpPost]
        public IActionResult PostMovie([FromBody]Movie movieObj)
        {
            _dbContext.Movies.Add(movieObj);

            if(_dbContext.SaveChanges() > 0)
            {
                return Ok(new { msg = "movie added successfully" });
            }

            return BadRequest("something went wrong");
        }

        [HttpPut]
        public IActionResult PutMovie([FromBody] Movie movie)
        {

            var movieinfo = _dbContext.Movies.FirstOrDefault(x => x.Id == movie.Id);

            if (movieinfo != null)
            {
                movieinfo.ReleaseDate = movie.ReleaseDate;
                movieinfo.Title = movie.Title;
                movieinfo.Description = movie.Description;
                
                _dbContext.SaveChanges();
            }

            return Ok("updated succesfully");
        }

        [HttpDelete]
        public IActionResult DeleteMovie([FromQuery] long id)
        {

            var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movie != null)
            {
                return Ok(_dbContext.Movies.Remove(movie));
            }

            return BadRequest("something went wrong");
            
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
