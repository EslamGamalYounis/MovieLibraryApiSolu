using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLibraryApi.Models;
using MovieLibraryApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult Add(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            movieRepo.Add(movie);
            return Content("Added Successfully");
        }

        [HttpPut("{movie}/{id}")]
        public IActionResult Put(Movie movie,int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            movieRepo.Edit(movie);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {
                movieRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }


    }
}
