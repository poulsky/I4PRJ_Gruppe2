using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BargainBarter.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Bytteannonce : DbContext
    {
        // Your context has been configured to use a 'Bytteannonce' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'BargainBarter.Models.Bytteannonce' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Bytteannonce' 
        // connection string in the application configuration file.
        public Bytteannonce()
            : base("name=Bytteannonce")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<BytteAnnonce> ByttenAnnonces { get; set; }
        public virtual DbSet<Brugerprofil> Brugerprofils { get; set; }
        public virtual DbSet<Adresse> Adresses { get; set; }
    }


    public class BytteAnnonce
    {
        public int BytteAnnonceId { get; set; }
        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        public byte[] Billede { get; set; }
    }

    public class Brugerprofil
    {
        [Key]
        public string Email { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public Adresse Adresse { get; set; }
        public List<BytteAnnonce> BytteAnnonces { get; set; }
    }

    public class Adresse
    {
        public int AdresseId { get; set; }
        public string Vejnavn { get; set; }
        public string Husnummer { get; set; }
        public string Postnummer { get; set; }
        public string Bynavn { get; set; }
    }
}