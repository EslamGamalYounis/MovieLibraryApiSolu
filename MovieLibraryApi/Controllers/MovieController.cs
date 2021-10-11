using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieLibraryApi.Models;
using MovieLibraryApi.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class MovieController : ControllerBase
    {
        public IMovieRepository movieRepo { get; set; }

        public MovieController(IMovieRepository movieRepository)
        {
            this.movieRepo = movieRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = movieRepo.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var movie = movieRepo.GetById(id);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost("{movie}")]
        public IActionResult Add([FromForm]Movie movie)
        {
            var file = Request.Form.Files[0];

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (file != null)
            {
                using (var datastream = new MemoryStream())
                {
                    file.CopyToAsync(datastream);
                    movie.Image = datastream.ToArray();
                }
            }
            Movie newMovie = new Movie { Name=movie.Name,Description=movie.Description,ReleaseDate=movie.ReleaseDate,Image=movie.Image};
            movieRepo.Add(newMovie);
            return Content("Added Successfully");
        }

        [HttpPut("{movie}/{id}")]
        public IActionResult Put([FromForm]Movie movie,int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            Movie newMovie = new Movie { Name = movie.Name, Description = movie.Description, ReleaseDate = movie.ReleaseDate, Image = movie.Image };
            movieRepo.Edit(newMovie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var movie = movieRepo.GetById(id);
                if (movie == null)
                {
                    return NotFound();
                }

                movieRepo.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Internal server error"+ex);
            }
        }


    }
}
