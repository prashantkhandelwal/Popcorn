﻿using Popcorn.Models;

namespace Popcorn.Repositories
{
    public interface IMoviesRepository
    {
        Task<IExecutable<Movie>> SearchMovies(string MovieName);

        Task<IExecutable<Credits>> GetCredits(int MovieId);

        Task<IExecutable<Credits>> GetMoviesDirectedBy(string DirectorName);

        Task<IExecutable<Movie>> GetMovieById(int MovieId);

        Task<IExecutable<Movie>> GetMovieById(object? MovieId);

        Task<IExecutable<Keywords>> GetKeywords(int MovieId);
    }
}
