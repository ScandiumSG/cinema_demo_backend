using cinemaServer.Models.PureModels;

namespace cinemaServer.Data
{
    public class DatabaseSeeder
    {
        private Random _rng;

        private List<Movie> _movieList = new List<Movie>();


        public DatabaseSeeder(int randomSeed, int numberOfMovies) 
        {
            _rng = new Random(randomSeed);
            GenerateMovies(numberOfMovies);
        }

        private void GenerateMovies(int numberOfMovies) 
        {
            Array ratingValues = Enum.GetValues(typeof(ERatings));
            for (int i = 1; i < numberOfMovies + 1; i++) 
            {
                Movie newMovie = new Movie()
                {
                    Id = i,
                    Title = "Some Title",
                    Description = "A description",
                    Runtime = _rng.Next(45, 390),
                    Year = _rng.Next(1950, DateTime.Now.Year + 1),
                    Rating = (ERatings)ratingValues.GetValue(_rng.Next(ratingValues.Length))
                };
                _movieList.Add(newMovie);
            }
        }

        public List<Movie> Movies { get { return _movieList; } }
    }
}
