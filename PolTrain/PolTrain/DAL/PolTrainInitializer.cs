using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Poltrain.Models;
using PolTrain.Models;

namespace PolTrain.DAL
{
    public class PolTrainInitializer : DropCreateDatabaseAlways<PolTrainContext>
    {
        protected override void Seed(PolTrainContext context)
        {
            SeedStoreData(context);
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        private void SeedStoreData(PolTrainContext context)
        {
            var stations = new List<Station>
            {
                new Station {City = "Wrocław", Name = "Wrocław Główny"},
                new Station {City = "Wrocław", Name = "Wrocław Psie Pole"},
                new Station {City = "Warszawa", Name = "Warszawa Centralna"},
                new Station {City = "Warszawa", Name = "Warszawa Zachodnia"},
                new Station {City = "Warszawa", Name = "Warszawa Wschodnia"},
                new Station {City = "Łódź", Name = "Łódź Kaliska"},
                new Station {City = "Łódź", Name = "Łódź Widzew"},
                new Station {City = "Łódź", Name = "Łódź Fabryczna"},
                new Station {City = "Zakopane", Name = "Zakopane"},
                new Station {City = "Gdańsk", Name = "Gdańsk"},
                new Station {City = "Opole", Name = "Opole Główne"},
                new Station {City = "Katowice", Name = "Katowice"},
                new Station {City = "Częstochowa", Name = "Częstochowa Główna"},
                new Station {City = "Częstochowa", Name = "Częstochowa Stradom"},
                new Station {City = "Konin", Name = "Konin"}
            };
            stations.ForEach(g => context.Stations.AddOrUpdate(g));
            context.SaveChanges();

            var tracks = new List<Track>
            {
                new Track
                {
                    StartStationId = 9,
                    EndStationId = 6,
                    TrackName = "Mieszko",
                    Length = 322,
                    SeatsAmt = 400,
                    Diner = true,
                    NormalPrice = 85,
                    JourneyTime = new TimeSpan(0, 2, 52, 0),
                    ClassLevel = ClassLevel.SecondClass
                },
                new Track
                {
                    StartStationId = 5,
                    EndStationId = 3,
                    TrackName = "Konopnicka",
                    Length = 322,
                    SeatsAmt = 400,
                    Diner = true,
                    NormalPrice = 85,
                    JourneyTime = new TimeSpan(0, 2, 52, 0),
                    ClassLevel = ClassLevel.SecondClass
                },
                new Track
                {
                    StartStationId = 8,
                    EndStationId = 15,
                    TrackName = "Tuwim",
                    Length = 322,
                    SeatsAmt = 400,
                    Diner = true,
                    NormalPrice = 85,
                    JourneyTime = new TimeSpan(0, 2, 45, 0),
                    ClassLevel = ClassLevel.SecondClass
                }
            };

            tracks.ForEach(s => context.Tracks.AddOrUpdate(s));
            context.SaveChanges();


            var lines = new List<Line>
            {
                new Line()
                {
                    Arrival = new DateTime(2017, 01, 24, 11, 43, 12),
                    Departure = new DateTime(2017, 01, 24, 14, 43, 12),
                    ReservedSeatsAmt = 100,
                    TrackId = 2
                },
                new Line()
                {
                    Arrival = new DateTime(2016, 01, 24, 11, 43, 12),
                    Departure = new DateTime(2016, 01, 24, 14, 43, 12),
                    ReservedSeatsAmt = 120,
                    TrackId = 1
                },
                new Line()
                {
                    Arrival = new DateTime(2017, 01, 24, 09, 15, 00),
                    Departure = new DateTime(2017, 01, 24, 12, 00, 00),
                    ReservedSeatsAmt = 120,
                    TrackId = 3
                },
                new Line()
                {
                    Arrival = new DateTime(2017, 01, 24, 10, 15, 00),
                    Departure = new DateTime(2017, 01, 24, 13, 00, 00),
                    ReservedSeatsAmt = 120,
                    TrackId = 3
                },
                new Line()
                {
                    Arrival = new DateTime(2017, 01, 24, 11, 15, 00),
                    Departure = new DateTime(2017, 01, 24, 14, 00, 00),
                    ReservedSeatsAmt = 120,
                    TrackId = 3
                },
                new Line()
                {
                    Arrival = new DateTime(2017, 01, 24, 13, 15, 00),
                    Departure = new DateTime(2017, 01, 24, 16, 00, 00),
                    ReservedSeatsAmt = 120,
                    TrackId = 3
                },
                new Line()
                {
                    Arrival = new DateTime(2017, 01, 24, 16, 15, 00),
                    Departure = new DateTime(2017, 01, 24, 18, 00, 00),
                    ReservedSeatsAmt = 120,
                    TrackId = 3
                }

            };
            lines.ForEach(s => context.Lines.AddOrUpdate(s));
            context.SaveChanges();

            var concessions = new List<Concession>
            {
                new Concession()
                {
                    Discount = 0,
                    Label = "Brak ulgi"
                },
                new Concession()
                {
                    Discount = 51,
                    Label = "51% - Studencka"
                },
                new Concession()
                {
                    Discount = 100,
                    Label = "100% - Dla dzieci do lat 4"
                },
                new Concession()
                {
                    Discount = 37,
                    Label = "37% - Dzieci / Młodzież"
                },
                new Concession()
                {
                    Discount = 78,
                    Label = "78% - Żołnierze"
                },
            };
            concessions.ForEach(c => context.Concessions.AddOrUpdate(c));
            context.SaveChanges();
        }

        public static void InitializeIdentityForEF(PolTrainContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "adam.dlubak@gmail.com";
            const string password = "PolTrain!";
            const string roleName = "Admin";


            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = name,
                    Email = name,
                    UserData = new UserData()
                    {
                        FirstName = "Adam",
                        LastName = "Dłubak",
                        PhoneNumber = "669277242"
                    }
                };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            //var user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }

    }
}