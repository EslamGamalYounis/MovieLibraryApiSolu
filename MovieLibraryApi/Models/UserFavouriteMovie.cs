using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibraryApi.Models
{
    public class UserFavouriteMovie
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
