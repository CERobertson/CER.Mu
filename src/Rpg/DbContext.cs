namespace CER.Rpg
{
    using CER.Graphs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using ef = System.Data.Entity;

    public class DbContext : ef.DbContext
    {
        public DbContext()
            : this(new ef.CreateDatabaseIfNotExists<DbContext>()) { } 

        public DbContext(ef.IDatabaseInitializer<DbContext> strategy)
        {
            ef.Database.SetInitializer<DbContext>(strategy);
            strategy.InitializeDatabase(this);
        }
        #region seeded
        public ef.DbSet<creation_process> CreationProcesses { get; set; }
        #endregion

        #region unseeded
        public ef.DbSet<game> Games { get; set; }
        public ef.DbSet<player> Players { get; set; }
        public ef.DbSet<character> Characters { get; set; }
        public ef.DbSet<role> Roles { get; set; }
        public ef.DbSet<performance> Performances { get; set; }
        public ef.DbSet<plot> Plots { get; set; }
        public ef.DbSet<relationship> Relationships { get; set; }
        public ef.DbSet<location> Locations { get; set; }
        #endregion

        protected override void OnModelCreating(ef.DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public IEnumerable<element> Elements()
        {
            return this.Games.ToList<element>().Union(
                this.Players.ToList<element>()).Union(
                this.Characters.ToList<element>()).Union(
                this.Roles.ToList<element>()).Union(
                this.Performances.ToList<element>()).Union(
                this.Plots.ToList<element>()).Union(
                this.Relationships.ToList<element>()).Union(
                this.Locations.ToList<element>());
        }
    }

    public class game : element
    {
        public int current_chapter { get; set; }
        public virtual List<plot> chapters { get; set; }
        public virtual List<performance> performances { get; set; }
    }
    public class player : element
    {
        public string name { get; set; }
    }
    public class character : element
    {
        public virtual List<relationship> relationships { get; set; }
    }
    public class role : element
    {
        public virtual player player { get; set; }
        public virtual character character { get; set; }

    }
    public class performance : element
    {
        public string start { get; set; }
        public decimal duration { get; set; }
        public virtual role role { get; set; }
    }
    public class plot : element
    {
        public string introduction { get; set; }
        public decimal estimated_duration { get; set; }
        public int scale_duration { get; set; }
        public decimal confidence_duration { get; set; }
        public virtual List<plot> subplots { get; set; }
        public virtual List<character> characters { get; set; }
        public virtual List<relationship> relationships { get; set; }
        public virtual List<performance> participants { get; set; }
    }
    public class relationship : element
    {
        public int priority { get; set; }
        public virtual List<character> Characters { get; set; }
    }
    public class location : element { }
    public class creation_process
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string video { get; set; }
        public virtual List<game> Games { get; set; }
        public virtual List<player> Players { get; set; }
        public virtual List<character> Characters { get; set; }
        public virtual List<role> Roles { get; set; }
        public virtual List<performance> Performances { get; set; }
        public virtual List<plot> Plots { get; set; }
        public virtual List<relationship> Relationships { get; set; }
        public virtual List<location> Locations { get; set; }
    }    
    public abstract class element : Entity
    {
        private static string InitialContext;
        static element()
        {
            element.InitialContext = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        public element()
        {
            this.partition = element.InitialContext;
        }

        [Key]
        public int id { get; set; }
        public string partition { get; set; }
        public string gm_name { get; set; }
        public string description { get; set; }
        public virtual List<creation_process> creation_history { get; set; }

        [NotMapped]
        public override string variable { get { return this.gm_name; } set { this.gm_name = value; }
        }
    }
}

