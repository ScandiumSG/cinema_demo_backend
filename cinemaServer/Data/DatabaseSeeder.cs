using cinemaServer.Models.PureModels;
using cinemaServer.Models.User;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System;

namespace cinemaServer.Data
{
    public class DatabaseSeeder
    {
        private Random _rng;

        private List<Movie> _movieList;
        private List<Theater> _theaterList;
        private List<Screening> _screeningList;

        public DatabaseSeeder(int randomSeed, int numberOfMovies, int numberOfTheaters, int numberOfScreenings) 
        {
            _rng = new Random(randomSeed);

            _movieList = new List<Movie>();
            _theaterList = new List<Theater>();
            _screeningList = new List<Screening>();

            GenerateMovies(numberOfMovies);
            GenerateTheaters(numberOfTheaters);
            GenerateScreenings(numberOfScreenings);
        }

        private void GenerateMovies(int numberOfMovies) 
        {
            Array ratingValues = Enum.GetValues(typeof(ERatings));
            for (int i = 1; i < numberOfMovies; i++) 
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

        private void GenerateTheaters(int numberOfTheaters) 
        {
            for (int i = 1; i < numberOfTheaters; i++) 
            {
                Theater newTheater = new Theater()
                {
                    Id = i,
                    Capacity = _rng.Next(8, 150),
                    Name = "A theater name"
                };
                _theaterList.Add(newTheater);
            }
        }

        private void GenerateScreenings(int numberOfScreenings) 
        {
            for (int i = 1; i < numberOfScreenings; i++) 
            {
                Movie movie = _movieList[_rng.Next(0, _movieList.Count)];
                Theater theater = _theaterList[_rng.Next(0, _theaterList.Count)];
                Screening newScreening = new Screening()
                {
                    Id = i,
                    MovieId = movie.Id,
                    TheaterId = theater.Id,
                    StartTime = new DateTime()
                        .AddYears(_rng.Next(1999, 2025))
                        .AddDays(_rng.Next(0, 365))
                        .AddHours(_rng.Next(10, 25))
                        .ToUniversalTime()
                };
                _screeningList.Add(newScreening);
            }
        }

        public List<ApplicationUser> GeneratePredefinedUsers() 
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            // Identity Users
            //Admin
            ApplicationUser admin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Adminuser",
                NormalizedUserName = "ADMINUSER",
                Email = "Admin@cinema.com",
                NormalizedEmail = "ADMIN@CINEMA.COM",
                EmailConfirmed = true,
                SecurityStamp = GenerateSecurityStamp(32),
                Role = ERole.Admin
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, "adminpassword");


            ApplicationUser testUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "testuser",
                NormalizedUserName = "TESTUSER",
                Email = "test@user.com",
                NormalizedEmail = "TEST@USER.COM",
                EmailConfirmed = true,
                SecurityStamp = GenerateSecurityStamp(32),
                Role = ERole.User,
            };

            testUser.PasswordHash = passwordHasher.HashPassword(testUser, "password");

            List<ApplicationUser> users = new List<ApplicationUser>{ admin, testUser };

            return users;
        }

        private string GenerateSecurityStamp(int length) 
        {
            string AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int randomIndex = _rng.Next(AllowedChars.Length);
                stringBuilder.Append(AllowedChars[randomIndex]);
            }
            return stringBuilder.ToString();
        }

        public List<Screening> Screenings { get { return _screeningList; } }
        public List<Theater> Theaters { get { return _theaterList; } }
        public List<Movie> Movies { get { return _movieList; } }
    }
}
