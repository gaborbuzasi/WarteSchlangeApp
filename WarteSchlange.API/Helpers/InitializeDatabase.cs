using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.Helpers
{
    public static class InitializeDatabase
    {
        public static void Initialize(MainContext context)
        {
            if (context.Queues.Any())
            {
                return;
            }

            var image = new ImagesModel {
                Url = "http://via.placeholder.com/350x150"
            };

            context.Images.Add(image);
            context.SaveChanges();

            var company = new CompanyModel
            {
                Category = "Doctor",
                Email = "doctors@zkmsurgery.de",
                Location = "Karlsruhe, ZKM",
                Name = "ZKM Surgery GmbH",
                Phone = "+490123456789",
                Website = "https://zkmsurgery.de",
                ImageId = image.Id
            };

            var company2 = new CompanyModel
            {
                Category = "Utilities",
                Email = "info@sauermacher.de",
                Location = "Munchen",
                Name = "Sauermacher GmbH",
                Phone = "+4901234534789",
                Website = "https://sauchermacher.de",
                ImageId = image.Id
            };

            var openingTime = new OpeningTimeModel
            {
                Open = DateTime.Now.AddHours(-5),
                Close = DateTime.Now.AddHours(+25)
            };

            context.Companies.Add(company);
            context.Companies.Add(company2);
            context.OpeningTimes.Add(openingTime);
            context.SaveChanges();

            var queue = new QueueModel
            {
                AllowMultipleEntries = false,
                AverageWaitTimeSeconds = 600,
                CompanyId = company.Id,
                Description = "Doctor's queue",
                ImageId = image.Id,
                Location = "Room 18/B",
                Name = "Dr. Peter Artoli's queue",
                MaxLength = 20,
                OpeningTimeId = openingTime.Id,
                RequireSignup = false,
                AtTheReadyTimeout = 300,
                AtTheReadyCount = 1
            };

            var queue2 = new QueueModel
            {
                AllowMultipleEntries = false,
                AverageWaitTimeSeconds = 400,
                CompanyId = company2.Id,
                Description = "Utility bill queue",
                ImageId = image.Id,
                Location = "Room 1/A",
                Name = "Customer services queue",
                MaxLength = 10,
                OpeningTimeId = openingTime.Id,
                RequireSignup = false,
                AtTheReadyTimeout = 120,
                AtTheReadyCount = 1
            };

            context.Queues.Add(queue);
            context.SaveChanges();

            var user = new UserModel {
                Email = "user@address.com",
                Name = "IamYourFirstUser",
                Phone = "+49001912836",
                CompanyId = company.Id
            };

            var user2 = new UserModel
            {
                Email = "usersemail@address.com",
                Name = "IamYourSecondUser",
                Phone = "+49001912336",
                CompanyId = company2.Id
            };

            var user3 = new UserModel
            {
                Email = "usersemail@address.com",
                Name = "IamYourThirdUser",
                Phone = "+490019123836",
                CompanyId = company.Id
            };

            context.Users.Add(user);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.SaveChanges();

            InitializeMetaData(context);
        }

        private static void InitializeMetaData(MainContext context)
        {
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "red",
                Keyword2 = "cow"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "blue",
                Keyword2 = "sheep"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "green",
                Keyword2 = "rabbit"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "pink",
                Keyword2 = "giraffe"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "white",
                Keyword2 = "monkey"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "black",
                Keyword2 = "gorilla"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "turquoise",
                Keyword2 = "orangutan"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "orange",
                Keyword2 = "seagull"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "grey",
                Keyword2 = "eagle"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "silver",
                Keyword2 = "kangaroo"
            });
            context.Metadata.Add(new MetadataModel
            {
                Keyword1 = "cian",
                Keyword2 = "ant"
            });

            context.SaveChanges();
        }
    }

}
