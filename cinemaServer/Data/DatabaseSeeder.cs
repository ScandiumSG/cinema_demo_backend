﻿using cinemaServer.Data.SeedData;
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
        private List<TicketType> _ticketTypeList;
        private List<Seat> _seatsList;

        public DatabaseSeeder(int randomSeed, int numberOfMovies, int numberOfTheaters, int numberOfCustomers, int numberOfScreenings, int numberOfTickets) 
        {
            _rng = new Random(randomSeed);

            _movieList = new List<Movie>();
            _theaterList = new List<Theater>();
            _screeningList = new List<Screening>();
            _customerList = new List<ApplicationUser>();
            _ticketList = new List<Ticket>();
            _ticketTypeList = new List<TicketType>();
            _seatsList = new List<Seat>();

            GenerateCustomers(numberOfCustomers);
            GenerateTheaters(numberOfTheaters);
            GenerateMovies(numberOfMovies);
            GenerateScreenings(numberOfScreenings);
            GenerateTicketTypes();
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
                    Title = $"Movie Title {i}",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean elit arcu, auctor et suscipit sed, posuere id sapien. Etiam tincidunt lorem dictum pretium faucibus. Quisque vel accumsan ligula, et cursus ligula. Morbi mattis mauris vel rhoncus finibus. Nullam aliquet tempus nunc, ut elementum augue ultrices at. Vestibulum tincidunt, erat sit amet tempus eleifend, eros lorem volutpat risus, a varius mauris lectus id quam. Donec pharetra nibh id eleifend tincidunt. Praesent non ipsum bibendum, convallis sem at, lacinia odio. Vivamus ultrices rhoncus magna et vestibulum. Donec eleifend ipsum lectus, at accumsan erat elementum vel. Mauris dui quam, feugiat id euismod vitae, ultrices at nisl. Sed rhoncus eros et orci fringilla, ac porta nisl venenatis. ",
                    Runtime = _rng.Next(45, 390),
                    Year = _rng.Next(1990, DateTime.Now.Year + 1),
                    Rating = (ERatings?)ratingValues.GetValue(_rng.Next(ratingValues.Length)),
                    AverageRating = 5 - Math.Round(_rng.NextDouble() * _rng.Next(1,5), 1)
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

            for (int i = 1; i <= numberOfSeats; i++) 
            {
                int RowNumber = (i - 1) / seatPerRow + 1;
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
                        .AddYears(_rng.Next(2015, 2025))
                        .AddDays(_rng.Next(0, 365))
                        .AddHours(_rng.Next(10, 24))
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
            string adminGUID = PredefinedGuid.GetDefinedGUID(_rng);
            ApplicationUser admin = new ApplicationUser
            {
                Id = adminGUID,
                UserName = "adminuser",
                NormalizedUserName = "ADMINUSER",
                Email = "admin@cinema.com",
                NormalizedEmail = "ADMIN@CINEMA.COM",
                EmailConfirmed = true,
                SecurityStamp = GenerateSecurityStamp(32),
                Role = ERole.Admin
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, "adminpassword");

            string userGUID = PredefinedGuid.GetDefinedGUID(_rng);

            ApplicationUser testUser = new ApplicationUser
            {
                Id = userGUID,
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
            if (numberOfCustomers > 198) 
            {
                throw new ArgumentException("Cant generate more than 198 customers");
            }

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            _customerList.AddRange(GeneratePredefinedUsers());
            numberOfCustomers -= _customerList.Count;

            for (int i = 1; i < numberOfCustomers; i++) 
            {
                string customerGUID = PredefinedGuid.GetDefinedGUID(_rng);
                ApplicationUser newUser = new ApplicationUser
                {
                    Id = customerGUID,
                    UserName = $"user{i}",
                    NormalizedUserName = $"user{i}".ToUpper(),
                    Email = $"user{i}@cinema.com",
                    NormalizedEmail = $"user{i}@cinema.com".ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = GenerateSecurityStamp(32),
                    Role = ERole.User,
                    LockoutEnabled = true,
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

        private void GenerateTicketTypes() 
        {
            List<TicketType> types = new List<TicketType>()
            {
                new TicketType()
                {
                    Id = 1,
                    Name = "Standard",
                    Description = "Standard ticket for people between the age of 18 and 65.",
                    Price = 5900,
                    IsActive = true,
                },
                new TicketType()
                {
                    Id = 2,
                    Name = "Student",
                    Description = "Requires a valid student ID.",
                    Price = 3900,
                    IsActive = true,
                },
                new TicketType()
                {
                    Id = 3,
                    Name = "Child",
                    Description = "For people between the age of 3 and 18",
                    Price = 19900,
                    IsActive = true,
                },
                new TicketType()
                {
                    Id = 4,
                    Name = "Senior",
                    Description = "For people over the age of 65.",
                    Price = 3900,
                    IsActive = true,
                },
                new TicketType()
                {
                    Id = 5,
                    Name = "Toddler",
                    Description = "For children under the age of 3.",
                    Price = 59900,
                    IsActive = true,
                },
            };
            _ticketTypeList.AddRange(types);
        }

        public void GenerateTickets(int numberOfTickets) 
        {
            for (int i = 1; i < numberOfTickets; i++)
            {
                Screening screening = _screeningList[_rng.Next(_screeningList.Count)];
                string customerId = _customerList[_rng.Next(_customerList.Count)].Id;
                List<Seat> AvailableSeats = _seatsList.Where(s => s.TheaterId == screening.TheaterId).ToList();
                Seat Seat = AvailableSeats.ElementAt(_rng.Next(AvailableSeats.Count));

                int ticketTypeDeterminer = _rng.Next(1,101);
                TicketType type;
                if (ticketTypeDeterminer == 1)
                {
                    type = _ticketTypeList[4];
                } else if (ticketTypeDeterminer < 5)
                {
                    type = _ticketTypeList[3];
                } else if (ticketTypeDeterminer < 20)
                {
                    type = _ticketTypeList[2];
                } else if (ticketTypeDeterminer < 50)
                {
                    type = _ticketTypeList[1];
                } else 
                {
                    type = _ticketTypeList[0];
                }

                Ticket newTicket = new Ticket()
                {
                    Id = i,
                    ScreeningId = screening.Id,
                    MovieId = screening.MovieId,
                    TheaterId = screening.TheaterId,
                    SeatId = Seat.Id,
                    CustomerId = customerId,
                    TicketTypeId = type.Id,
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
        public List<TicketType> TicketTypes { get { return _ticketTypeList; } }
    }
}
