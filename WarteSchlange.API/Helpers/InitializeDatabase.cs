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

            var openingTime = new OpeningTimeModel
            {
                Open = DateTime.Now.AddHours(-5),
                Close = DateTime.Now.AddHours(+25)
            };

            context.Companies.Add(company);
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
                RequireSignup = false
            };

            context.Queues.Add(queue);
            context.SaveChanges();

            var user = new UserModel {
                Email = "usersemail@address.com",
                Name = "IamYourFirstUser",
                Phone = "+49001912836",
                CompanyId = company.Id
            };

            context.Users.Add(user);
            context.SaveChanges();

            var queueEntry = new QueueEntryModel
            {
                EntryTime = DateTime.Now,
                QueueId = queue.Id,
                IdentificationCode = QueueHelper.GenerateQueueIdentification(queue.Id, context),
                Priority = 0,
                UserId = user.Id,
            };

            context.QueueEntries.Add(queueEntry);
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
