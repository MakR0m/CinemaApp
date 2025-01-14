using CinemaApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Core.Interfaces
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<Movie>> GetAllAsync();
        public Task<Movie> GetByIdAsync(int id);
        public Task AddAsync(Movie movie);
        public Task UpdateAsync(Movie movie);
        public Task DeleteAsync(int id);
    }
}
