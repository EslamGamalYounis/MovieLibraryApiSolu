using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibraryApi.Models
{
    public enum Rule
    {
        Admin,
        User
    }
    public class User
    {
        public int Id{ get; set; }
        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public Rule Rule { get; set; }
        public byte[] Image { get; set; }
        public virtual IEnumerable<UserFavouriteMovie> FavMovies { get; set; }

    }
}
