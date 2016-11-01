using System.Collections.Generic;
using BargainBarterV2.Models;
using Microsoft.AspNet.Identity;

namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BargainBarterV2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private byte[] Readpicture(string filename)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(filename);

            return bytes;
        }

        protected override void Seed(BargainBarterV2.Models.ApplicationDbContext context)
        {

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password@123");
            

            #region SeedingUsers

            ApplicationUser user1 = new ApplicationUser
            {
                UserName = "Steve@Steve.com",
                PasswordHash = password,
                PhoneNumber = "12349873",
                PhoneNumberConfirmed = true,
                Email = "Steve@Steve.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Firstname = "Steve",
                Lastname = "Hansen",
                Address = new Address()
                {
                    StreetName = "Jensbaggesensvej",
                    City = "Aarhus",
                    PostalCode = "8200",
                    StreetNumber = "13",
                    Coordinate = new Coordinates()
                    
                },
                BarterAdds = new List<BarterAdd>()

            }; 

            ApplicationUser user2 = new ApplicationUser
            {
                UserName = "Jens@go.com",
                PasswordHash = password,
                PhoneNumber = "15150882",
                PhoneNumberConfirmed = true,
                Email = "Jens@go.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Firstname = "Jens",
                Lastname = "Didriksen",
                Address = new Address()
                {
                    StreetName = "Bredgade",
                    City = "Tilst",
                    PostalCode = "4200",
                    StreetNumber = "111",
                    Coordinate = new Coordinates()
                },
                 BarterAdds = new List<BarterAdd>()
            };


            ApplicationUser user3 = new ApplicationUser
            {
                UserName = "Jakob@go.com",
                PasswordHash = password,
                PhoneNumber = "15150883",
                PhoneNumberConfirmed = true,
                Email = "Jakob@go.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Firstname = "Jakob",
                Lastname = "Andersen",
                Address = new Address()
                {
                    StreetName = "Nygade",
                    City = "Aalborg",
                    PostalCode = "8410",
                    StreetNumber = "7",
                    Coordinate = new Coordinates()
                },
                 BarterAdds = new List<BarterAdd>()
            };

            ApplicationUser user4 = new ApplicationUser
            {
                UserName = "Søren@go.com",
                PasswordHash = password,
                PhoneNumber = "15150884",
                PhoneNumberConfirmed = true,
                Email = "Søren@go.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Firstname = "Søren",
                Lastname = "Holm",
                Address = new Address()
                {
                    StreetName = "Berstoftvej",
                    City = "Malling",
                    PostalCode = "8290",
                    StreetNumber = "265",
                    Coordinate = new Coordinates()
                },
                 BarterAdds = new List<BarterAdd>()
            };        
            #endregion

            #region SeedingAds

            BarterAdd ad1 = new BarterAdd()
            {
                Titel = "Boremaskine",
                Description = "Bosch reklamerer selv med at den er til gør-det-selv manden," +
                              " men du skal vide at du også går på kompromis med både kvalitet" +
                              " og holdbarhed. Jeg har selv haft flere grønne fra Bosch og kan" +
                              " konstatere at pris og kvalitet hænger sammen. Eksempelvis har jeg" +
                              " nogle gange oplevet at aklsen er blevet skæv, og så er det umuligt" +
                              " at bore et lige hul.",
                Category = "Elektronik",
                CreatedDateTime = new DateTime(2016, 8, 4, 12, 30, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\bosch-gron-boremaskine.png"),

                Thumbnail =
                    Helperfunctions.Helper.MakeThumbnail(
                        Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\bosch-gron-boremaskine.png"), 320,
                        150)
            };


            BarterAdd ad2 = new BarterAdd()
            {
                Titel = "Arduino nano",
                Description = "The Arduino Nano is a small, complete, and breadboard-friendly" +
                              " board based on the ATmega328 (Arduino Nano 3.x) or ATmega168 " +
                              "(Arduino Nano 2.x). It has more or less the same functionality of the " +
                              "Arduino Duemilanove, but in a different package. It lacks only a" +
                              " DC power jack, and works with a Mini-B USB cable instead of a " +
                              "standard one. The Nano was designed and is being produced by Gravitech.",
                Category = "Elektronik",
                CreatedDateTime = new DateTime(2016, 8, 9, 12, 30, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\ArduinoNanoFront.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\ArduinoNanoFront.jpg"), 320, 150),
                
            };


            BarterAdd ad3 = new BarterAdd()
            {
                Titel = "Arduino Mega",
                Description = "The Mega 2560 is a microcontroller board based on the ATmega2560." +
                              " It has 54 digital input/output pins (of which 15 can be used as PWM " +
                              "outputs), 16 analog inputs, 4 UARTs (hardware serial ports), a 16 MHz " +
                              "crystal oscillator, a USB connection, a power jack, an ICSP header, and " +
                              "a reset button. It contains everything needed to support the " +
                              "microcontroller; simply connect it to a computer with a USB cable or" +
                              " power it with a AC-to-DC adapter or battery to get started. The Mega " +
                              "2560 board is compatible with most shields designed for the Uno and the " +
                              "former boards Duemilanove or Diecimila.",
                Category = "Elektronik",
                CreatedDateTime = new DateTime(2016, 8, 11, 12, 30, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\ArduinoMega.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\ArduinoMega.jpg"), 320, 150)
            };
            BarterAdd ad4 = new BarterAdd()
            {
                Titel = "T-Shirt",
                Description = "Lækker hvid trøje fra H&M som er i helt vildt god stand," +
                              " men som er blevet for lille",
                Category = "Tøj",
                CreatedDateTime = new DateTime(2016, 9, 11, 12, 30, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\t-shirt.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\t-shirt.jpg"), 320, 150)
            };


            BarterAdd ad5 = new BarterAdd()
            {
                Titel = "Sweatshirt",
                Description = "Det er en sweatshirt i ret tøsede farver størrelsen er large" +
                              ", Den er lettere brugt og har et par huller",
                Category = "Tøj",
                CreatedDateTime = new DateTime(2016, 10, 11, 12, 30, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\sweatshirt.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\sweatshirt.jpg"), 320, 150),

            };
            BarterAdd ad6 = new BarterAdd()
            {
                Titel = "Sweatshirt",
                Description = "Det er en sweatshirt i ret tøsede farver størrelsen er large" +
                              ", Den er lettere brugt og har et par huller",
                Category = "Tøj",
                CreatedDateTime = new DateTime(2016, 10, 11, 12, 30, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\sweatshirt.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\sweatshirt.jpg"), 320, 150),

            };
            BarterAdd ad7 = new BarterAdd()
            {
                Titel = "Jeans",
                Description = "Usle dårlige jeans, som bare ikke er ret pæne, men de kan måske" +
                              "ordnes ",
                Category = "Tøj",
                CreatedDateTime = new DateTime(2016, 10, 12, 12, 30, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\jeans.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\jeans.jpg"), 320, 150),

            };
            BarterAdd ad8 = new BarterAdd()
            {

                Titel = "Hættetrøje",
                Description = " bluse med lange ærmer og rund hals, fremstillet i kraftig " +
                              "bomuld med en lodden vrangside, der bæres ind mod kroppen." +
                              " Sweatshirten er blevet brugt af sportsfolk gennem hele 1900-t.; " +
                              "fra 1960'erne blev den en del af den almindelige påklædning, og man" +
                              " begyndte at påtrykke navne, logoer, mønstre og billeder.",
                Category = "Tøj",
                CreatedDateTime = new DateTime(2016, 11, 12, 12, 30, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\hættetrøje.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\hættetrøje.jpg"), 320, 150),

            };

            BarterAdd ad9 = new BarterAdd()
            {

                Titel = "Klaverstol",
                Description = "En standard klaverstol, den kan justeres i højden og er rigtig god. " +
                              "Træet er lidt mørt men stolen har mange år i sig endnu",
                Category = "Interiør",
                CreatedDateTime = new DateTime(2016, 02, 01, 12, 30, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\klaverstol.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\klaverstol.jpg"), 320, 150),
            };
            BarterAdd ad10 = new BarterAdd()
            {

                Titel = "Spisebord",
                Description = "Et bord er et møbel bestående af en vandret flade af varierende materiale, " +
                              "som er hævet fra gulvhøjde, typisk vha. fire bordben. Bordet har det" +
                              " formål at man kan stille ting fra sig, eller at man kan sidde omkring" +
                              " det som et samlingssted, eller endda en kombination af begge. Der findes" +
                              " mange forskellige slags design af borde.",
                Category = "Interiør",
                CreatedDateTime = new DateTime(2016, 02, 03, 12, 29, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\spisebord.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\spisebord.jpg"), 320, 150),

            };

            BarterAdd ad11 = new BarterAdd()
            {

                Titel = "To mands Sofa",
                Description = "Min sofa er der som sådan ikke noget galt med, den er jeg vildt glad " +
                              "for, både i design og funktion. Det var DEN sofa jeg ville have også " +
                              "selvom den sprang vores daværende budget med 10.000 kr. Det er farven" +
                              " den er gal med. Jeg går altid efter en grå sofa, grå er en lækker farve" +
                              " og den passer til ALT. Med en grå sofa kan jeg skifte stil så tosset" +
                              " jeg vil, og den vil altid passe ind. ",
                Category = "Interiør",
                CreatedDateTime = new DateTime(2016, 04, 03, 12, 29, 45),

                Picture = Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\sofa.jpg"),

                Thumbnail = Helperfunctions.Helper.MakeThumbnail(Readpicture(@"BargainBarterV2\Content\img\SeedingAdsPictures\sofa.jpg"), 320, 150),

            };

            user1.BarterAdds.Add(ad1);
            user1.BarterAdds.Add(ad2);
            user2.BarterAdds.Add(ad3);
            user2.BarterAdds.Add(ad4);
            user2.BarterAdds.Add(ad5);
            user2.BarterAdds.Add(ad6);
            user3.BarterAdds.Add(ad7);
            user3.BarterAdds.Add(ad8);
            user4.BarterAdds.Add(ad9);
            user4.BarterAdds.Add(ad10);
            user4.BarterAdds.Add(ad11);

            context.Users.AddOrUpdate(u => u.UserName, user1);
            context.Users.AddOrUpdate(u => u.UserName, user2);
            context.Users.AddOrUpdate(u => u.UserName, user3);
            context.Users.AddOrUpdate(u => u.UserName, user4);
          
            #endregion

        }
    }
}
