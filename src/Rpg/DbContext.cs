namespace CER.Rpg
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ef = System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System;

    public class DbContext : ef.DbContext
    {
        public ef.DbSet<game> Games { get; set; }

        protected override void OnModelCreating(ef.DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
    public class game
    {
        [Key]
        public int id { get; set; }
        public string gm_name { get; set; }
        public string description { get; set; }
        public int current_chapter { get; set; }
        public virtual List<plot> chapters { get; set; }
        public virtual List<performance> performances { get; set; }
        public virtual List<creation_process> creation_history { get; set; }
    }
    public class player
    {
        [Key]
        public int id { get; set; }
        public string gm_name { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public virtual List<creation_process> creation_history { get; set; }
    }
    public class character
    {
        [Key]
        public int id { get; set; }
        public string gm_name { get; set; }
        public string description { get; set; }
        public virtual List<creation_process> creation_history { get; set; }
    }
    public class role
    {
        [Key]
        public int id { get; set; }
        public string gm_name { get; set; }
        public string description { get; set; }
        public virtual player player { get; set; }
        public virtual character character { get; set; }
        public virtual List<creation_process> creation_history { get; set; }

    }
    public class performance
    {
        [Key]
        public int id { get; set; }
        public string gm_name { get; set; }
        public string description { get; set; }
        public string start { get; set; }
        public decimal duration { get; set; }
        public virtual role role { get; set; }
    }
    public class plot
    {
        [Key]
        public int id { get; set; }
        public string gm_name { get; set; }
        public string description { get; set; }
        public string introduction { get; set; }
        public decimal estimated_duration { get; set; }
        public int scale_duration { get; set; }
        public decimal confidence_duration { get; set; }
        public virtual List<plot> subplots { get; set; }
        public virtual List<performance> participants { get; set; }
        public virtual List<creation_process> creation_history { get; set; }
    }
    public class creation_process
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string video { get; set; }
    }
}

