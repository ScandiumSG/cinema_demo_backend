using cinemaServer.Models.PureModels;
using cinemaServer.Models.User;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace cinemaServer.Data
{
    public class DatabaseSeeder
    {
        private Random _rng;

        private List<Movie> _movieList;
        private List<Theater> _theaterList;
        private List<Screening> _screeningList;
        private List<ApplicationUser> _customerList;
        private List<Ticket> _ticketList;
        private List<Seat> _seatsList;

        public DatabaseSeeder(int randomSeed, int numberOfMovies, int numberOfTheaters, int numberOfCustomers, int numberOfScreenings, int numberOfTickets) 
        {
            _rng = new Random(randomSeed);

            _movieList = new List<Movie>();
            _theaterList = new List<Theater>();
            _screeningList = new List<Screening>();
            _customerList = new List<ApplicationUser>();
            _ticketList = new List<Ticket>();
            _seatsList = new List<Seat>();

            GenerateMovies(numberOfMovies);
            GenerateTheaters(numberOfTheaters);
            GenerateCustomers(numberOfCustomers);
            GenerateScreenings(numberOfScreenings);
            GenerateTickets(numberOfTickets); 

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
                _seatsList.AddRange(GenerateTheaterSeats(newTheater.Capacity, newTheater.Id));
                _theaterList.Add(newTheater);
            }
        }

        private List<Seat> GenerateTheaterSeats(int numberOfSeats, int TheaterId)
        {
            List<Seat> seats = new List<Seat>();

            int rows = _rng.Next(3, Math.Max(6, numberOfSeats / 6));
            int seatPerRow = numberOfSeats / rows;

            for (int i = 1; i < numberOfSeats; i++) 
            {
                int RowNumber = i - 1 / seatPerRow + 1;
                int seatNumber = (i - 1) % seatPerRow + 1;

                Seat newSeat = new Seat() 
                {
                    Id = i,
                    Row = RowNumber,
                    SeatNumber = seatNumber,
                    TheaterId = TheaterId
                };
                seats.Add(newSeat);
            }
            return seats;
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

        private List<ApplicationUser> GeneratePredefinedUsers() 
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            // Identity Users
            //Admin
            ApplicationUser admin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "adminuser",
                NormalizedUserName = "ADMINUSER",
                Email = "admin@cinema.com",
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

        private void GenerateCustomers(int numberOfCustomers) 
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            _customerList.AddRange(GeneratePredefinedUsers());
            numberOfCustomers -= _customerList.Count;

            for (int i = 1; i < numberOfCustomers; i++) 
            {
                ApplicationUser newUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = $"user{i}",
                    NormalizedUserName = $"user{i}".ToUpper(),
                    Email = $"user{i}@cinema.com",
                    NormalizedEmail = $"user{i}@cinema.com".ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = GenerateSecurityStamp(32),
                    Role = ERole.User
                };

                newUser.PasswordHash = passwordHasher.HashPassword(newUser, "dummypassword");
                _customerList.Add(newUser);
            }
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


        public void GenerateTickets(int numberOfTickets) 
        {
            for (int i = 1; i < numberOfTickets; i++)
            {
                Screening screening = _screeningList[_rng.Next(_screeningList.Count)];
                string customerId = _customerList[_rng.Next(_customerList.Count)].Id;
                List<Seat> AvailableSeats = _seatsList.Where(s => s.TheaterId == screening.TheaterId).ToList();
                Seat Seat = AvailableSeats.ElementAt(_rng.Next(AvailableSeats.Count));

                Ticket newTicket = new Ticket()
                {
                    Id = i,
                    ScreeningId = screening.Id,
                    MovieId = screening.MovieId,
                    TheaterId = screening.TheaterId,
                    SeatId = Seat.Id,
                    CustomerId = customerId,
                };

                _ticketList.Add(newTicket);
            }
        }

        public List<Screening> Screenings { get { return _screeningList; } }
        public List<Theater> Theaters { get { return _theaterList; } }
        public List<Seat> Seats { get { return _seatsList; } }
        public List<Movie> Movies { get { return _movieList; } }
        public List<ApplicationUser> Customers { get { return _customerList; } }
        public List<Ticket> Tickets { get { return _ticketList; } }
    }
}
