using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using API.Model;

namespace API.Data
{
    public static class Seeder
    {
        public static void Seed(HrApplicationContext context)
        {
            var departments = new Department[]
              {
                new Department {DepartmentName = "Human resources"},
                new Department {DepartmentName = "Accounting"},
                new Department {DepartmentName = "Management"},
                new Department {DepartmentName = "Logistics"}
              };

            foreach (var department in departments)
            {
                context.Departments.Add(department);
            }

            context.SaveChanges();

            var depHr = context.Departments.FirstOrDefault(d => d.DepartmentName == "Human resources");
            var depAcc = context.Departments.FirstOrDefault(d => d.DepartmentName == "Accounting");
            var depMan = context.Departments.FirstOrDefault(d => d.DepartmentName == "Management");
            var depLog = context.Departments.FirstOrDefault(d => d.DepartmentName == "Logistics");


            var users = new User[]
            {
                new User
                {
                    Name = "Paul Franklin",
                    BirthDay =new DateTime(1982,10,28),
                    Contract = new Contract
                        {
                            GrossSalary = 3000,
                            NumberOfHolidays = 20
                        },
                    Department = depHr,
                    CreationDate = DateTime.Now
                },
                new User
                {
                    Name="Hubert Dupargne",
                    BirthDay =new DateTime(1989,9,15),
                    Contract = new Contract
                    {
                        GrossSalary = 3400,
                        NumberOfHolidays = 24
                    },
                    Department = depHr,
                    CreationDate = DateTime.Now
                },
                new User
                {
                    Name="Irene De Maerschalk",
                    BirthDay =new DateTime(1945,5,13),
                    Contract = new Contract
                    {
                        GrossSalary = 2600,
                        NumberOfHolidays = 20
                    },
                    Department = depHr,
                    CreationDate = DateTime.Now
                },
                new User
                {
                    Name="Jack Rousseau",
                    BirthDay =new DateTime(1992,3,19),
                    CreationDate = DateTime.Now
                },
                new User
                {
                    Name= "Paul Ryan",
                    BirthDay =new DateTime(1962,2,19),
                    Department = depAcc,
                    CreationDate = DateTime.Now
                },
                new User
                {
                    Name= "Réné Duchasse",
                    BirthDay =new DateTime(1973,2,12),
                    Department = depAcc,
                    CreationDate = DateTime.Now
                },
                new User
                {
                    Name="Lebron James",
                    BirthDay =new DateTime(1984,7,12),
                    Contract = new Contract
                    {
                        GrossSalary = 8400,
                        NumberOfHolidays = 28
                    },
                    Department = depMan,
                    CreationDate = DateTime.Now
                },
                new User
                {
                    Name="Jonathan Arnoys",
                    BirthDay =DateTime.Now,
                    Contract = new Contract
                    {
                        GrossSalary = 9032,
                        NumberOfHolidays = 34
                    },
                    Department = depMan,
                    CreationDate = DateTime.Now
                } ,
                new User
                {
                    Name="Ilse Deprez",
                    BirthDay =new DateTime(1952,5,13),
                    Department = depLog,
                    CreationDate = DateTime.Now
                },
                new User
                {
                    Name= "Ahmed Fadik",
                    BirthDay =new DateTime(1994,8,28),
                    Department = depLog,
                    CreationDate = DateTime.Now
                }
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();

            var userManagerHr = context.Users.FirstOrDefault(u => u.Name == "Paul Franklin");
            var userManagerAcc = context.Users.FirstOrDefault(u => u.Name == "Paul Ryan");
            var userManagerMan = context.Users.FirstOrDefault(u => u.Name == "Jonathan Arnoys");
            var userManagerLog = context.Users.FirstOrDefault(u => u.Name == "Ilse Deprez");

            depHr.Manager = userManagerHr;
            depAcc.Manager = userManagerAcc;
            depMan.Manager = userManagerMan;
            depLog.Manager = userManagerLog;

            context.SaveChanges();

            var paulFranklin = context.Users.FirstOrDefault(u => u.Name == "Paul Franklin");
            var hubert = context.Users.FirstOrDefault(u => u.Name == "Hubert Dupargne");
            var irene = context.Users.FirstOrDefault(u => u.Name == "Irene De Maerschalk");
            var jack = context.Users.FirstOrDefault(u => u.Name == "Jack Rousseau");
            var paulRyan = context.Users.FirstOrDefault(u => u.Name == "Paul Ryan");
            var rene = context.Users.FirstOrDefault(u => u.Name == "Réné Duchasse");
            var lebron = context.Users.FirstOrDefault(u => u.Name == "Lebron James");
            var jonathan = context.Users.FirstOrDefault(u => u.Name == "Jonathan Arnoys");
            var ilse = context.Users.FirstOrDefault(u => u.Name == "Ilse Deprez");
            var ahmed = context.Users.FirstOrDefault(u => u.Name == "Ahmed Fadik");

            var holidays = new Holiday[]
            {
                new Holiday
                {
                    User = paulFranklin,
                    StartDateTime = new DateTime(2018, 06, 01),
                    EndDateTime = new DateTime(2018, 06, 16),
                    IsApproved = true
                },
                new Holiday
                {
                    User = paulFranklin,
                    StartDateTime = new DateTime(2019, 07, 22),
                    EndDateTime = new DateTime(2019, 07, 28),
                    IsApproved = false
                },
                new Holiday
                {
                    User = hubert,
                    StartDateTime = new DateTime(2018, 04, 02),
                    EndDateTime = new DateTime(2018, 04, 08),
                    IsApproved = true
                },
                new Holiday
                {
                    User = hubert,
                    StartDateTime = new DateTime(2019, 07, 1),
                    EndDateTime = new DateTime(2019, 07, 8),
                    IsApproved = true
                },
                new Holiday
                {
                    User = irene,
                    StartDateTime = new DateTime(2019, 02, 02),
                    EndDateTime = new DateTime(2019, 02, 08),
                    IsApproved = true
                },
                new Holiday
                {
                    User = irene,
                    StartDateTime = new DateTime(2019, 07, 1),
                    EndDateTime = new DateTime(2019, 07, 13),
                    IsApproved = true
                },
                new Holiday
                {
                    User = jack,
                    StartDateTime = new DateTime(2019, 02, 02),
                    EndDateTime = new DateTime(2019, 02, 08),
                    IsApproved = true
                },
                new Holiday
                {
                    User = jack,
                    StartDateTime = new DateTime(2019, 07, 1),
                    EndDateTime = new DateTime(2019, 07, 13),
                    IsApproved = true
                },
                new Holiday
                {
                    User = paulRyan,
                    StartDateTime = new DateTime(2019, 10, 12),
                    EndDateTime = new DateTime(2019, 10, 14),
                    IsApproved = true
                },
                new Holiday
                {
                    User = paulRyan,
                    StartDateTime = new DateTime(2019, 12, 20),
                    EndDateTime = new DateTime(2019, 12, 26),
                    IsApproved = true
                },
                new Holiday
                {
                    User = rene,
                    StartDateTime = new DateTime(2019, 10, 12),
                    EndDateTime = new DateTime(2019, 10, 14),
                    IsApproved = true
                },
                new Holiday
                {
                    User = rene,
                    StartDateTime = new DateTime(2019, 12, 20),
                    EndDateTime = new DateTime(2019, 12, 26),
                    IsApproved = true
                },
                new Holiday
                {
                    User = rene,
                    StartDateTime = new DateTime(2019, 1, 20),
                    EndDateTime = new DateTime(2019, 1, 26),
                    IsApproved = true
                },
                new Holiday
                {
                User = rene,
                StartDateTime = new DateTime(2018, 1, 20),
                EndDateTime = new DateTime(2018, 1, 26),
                IsApproved = true
                },
                new Holiday
                {
                    User = lebron,
                    StartDateTime = new DateTime(2019, 06, 01),
                    EndDateTime = new DateTime(2019, 06, 16),
                    IsApproved = true
                },
                new Holiday
                {
                    User = lebron,
                    StartDateTime = new DateTime(2019, 07, 22),
                    EndDateTime = new DateTime(2019, 07, 28),
                    IsApproved = false
                },
                new Holiday
                {
                    User = jonathan,
                    StartDateTime = new DateTime(2019, 1, 20),
                    EndDateTime = new DateTime(2019, 1, 26),
                    IsApproved = true
                },
                new Holiday
                {
                    User = jonathan,
                    StartDateTime = new DateTime(2019, 10, 20),
                    EndDateTime = new DateTime(2019, 10, 26),
                    IsApproved = true
                },
                new Holiday
                {
                    User = ilse,
                    StartDateTime = new DateTime(2018, 1, 20),
                    EndDateTime = new DateTime(2018, 1, 26),
                    IsApproved = true
                },
                new Holiday
                {
                    User = ilse,
                    StartDateTime = new DateTime(2019, 4, 20),
                    EndDateTime = new DateTime(2019, 4, 26),
                    IsApproved = true
                },
                new Holiday
                {
                    User = ahmed,
                    StartDateTime = new DateTime(2019, 1, 20),
                    EndDateTime = new DateTime(2019, 1, 26),
                    IsApproved = true
                }
            };

            foreach (var holiday in holidays)
            {
                context.Holidays.Add(holiday);
            }

            context.SaveChanges();

            var workTimes = new WorkTime[]
            {
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = paulFranklin, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)},

                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = hubert, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)},

                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = irene, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)},

                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = jack, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)},

                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = paulRyan, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)},

                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = rene, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)},

                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = lebron, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)},

                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = jonathan, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)},

                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = ilse, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)},

                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 02, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 03, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 04, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 04, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 07, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 07, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 08, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 08, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 03, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 09, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 09, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 10, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 10, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 13, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 02, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 13, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 14, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 14, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 15, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 15, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 16, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 16, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 17, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 17, 16, 30, 30)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 18, 08, 04, 05)},
                new WorkTime {User = ahmed, WorkDateTime = new DateTime(2019, 01, 18, 16, 30, 30)}
            };

            context.Worktimes.AddRange(workTimes);

            context.SaveChanges();
        }
    }
}
