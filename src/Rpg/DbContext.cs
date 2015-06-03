namespace CER.Rpg
{
    using CER.EntityFramework;
    using CER.JudeaPearl;
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
        public DirectedAcyclicGraph<belief> SaveBeliefNetworkToCharacter(string json, character c)
        {

            var dg = new DirectedGraph(json);
            var dag_subgraph = new DirectedAcyclicGraph<belief>(dg);

            foreach (var n in dag_subgraph.ToList())
            {
                n.Value.variable = n.Key;
                n.Value.character = c;
                n.Value.partition = this.Partition;
            }

            foreach (var n in dag_subgraph.Roots)
            {
                this.CreaetOrRetrieve(this.Beliefs, x => x.variable == n.variable && x.character.id == n.character.id && x.partition == n.partition, n);
            }
            return dag_subgraph;
        }

        public List<hypothesis> SaveHypothesesToBelief(string json, belief b)
        {
            var condition_probablity = new ConditionalProbability(json);
            var hypothesis_list = new List<hypothesis>();
            int h_index = 0;
            this.Beliefs.ToArray();
            this.Hypotheses.ToArray();
            foreach (var h in condition_probablity)
            {
                var hypothesis = new hypothesis { belief = b, name = h_index.ToString(), partition = this.Partition, propositions = new List<proposition>(h.Count) };
                for (int i = 0; i < h.Count; i++)
                {
                    var proposition = new proposition {hypothesis = hypothesis, name= i.ToString(), value = decimal.Parse(h.ElementAt(i))};
                    proposition = this.CreaetOrRetrieve(
                        this.Propositions,
                        x => x.name == proposition.name &&
                             x.hypothesis.name == hypothesis.name,
                        proposition,
                        true);
                }
                hypothesis = this.CreaetOrRetrieve(
                    this.Hypotheses,
                    x => x.name == hypothesis.name &&
                         x.belief.variable == hypothesis.belief.variable,
                    hypothesis,
                    true);
                hypothesis_list.Add(hypothesis);
                h_index++;
            }
            return hypothesis_list;
        }

        public virtual string Partition { get; private set; }

        public virtual T CreaetOrRetrieve<T>(ef.DbSet<T> set, Func<T, bool> predicate, T obj = null, bool SaveOnCreate = true) where T : class, IHasIntId, IHasPartitionString
        {
            return this.CreaetOrRetrieve(set, predicate, (x) => x.id == obj.id && x.partition == obj.partition, obj, SaveOnCreate);
        }
            
        public T CreaetOrRetrieve<T>(ef.DbSet<T> set, Func<T, bool> create_predicate, Func<T, bool> retrieve_predicate, T obj = null, bool SaveOnCreate = true) where T : class, IHasIntId
        {
            if (obj.id == 0)
            {
                return this.FirstOrCreate(set, create_predicate, obj, SaveOnCreate);
            }
            else
            {
                return set.Single(retrieve_predicate);
            }
        }
        public T FirstOrCreate<T>(ef.DbSet<T> set, Func<T, bool> predicate, T obj = null, bool SaveOnCreate = true) where T : class
        {
            T result;
            try
            {
                result = set.First(predicate);
            }
            catch (InvalidOperationException)
            {
                set.Add(obj);
                if (SaveOnCreate)
                {
                    this.SaveChanges();
                }
                result = obj;
            }
            return result;
        }

        public T SingleOrCreate<T>(ef.DbSet<T> set, T obj = null, bool SaveOnCreate = false) where T : element
        {
            return this.CreaetOrRetrieve(set, x => x.gm_name == obj.gm_name && x.partition == obj.partition, obj, SaveOnCreate);
        }
        
        public static string InitialContext;
        static DbContext()
        {
            DbContext.InitialContext = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public DbContext()
            : this(new ef.CreateDatabaseIfNotExists<DbContext>()) { }

        public DbContext(ef.IDatabaseInitializer<DbContext> strategy)
        {
            ef.Database.SetInitializer<DbContext>(strategy);
            strategy.InitializeDatabase(this);
        }

        public ef.DbSet<belief> Beliefs { get; set; }
        public ef.DbSet<character> Characters { get; set; }
        public ef.DbSet<game> Games { get; set; }
        public ef.DbSet<hypothesis> Hypotheses { get; set; }
        public ef.DbSet<location> Locations { get; set; }
        public ef.DbSet<performance> Performances { get; set; }
        public ef.DbSet<player> Players { get; set; }
        public ef.DbSet<plot> Plots { get; set; }
        public ef.DbSet<proposition> Propositions { get; set; }
        public ef.DbSet<relationship> Relationships { get; set; }
        public ef.DbSet<role> Roles { get; set; }

        public ef.DbSet<creation_process> CreationProcesses { get; set; }
        
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
        public character()
        {
            this.relationships = new List<relationship>();
            this.beliefs = new List<belief>();
        }

        public virtual List<relationship> relationships { get; set; }
        public virtual List<belief> beliefs { get; set; }
    }
    public class belief : Node<belief>, IHasIntId, IHasPartitionString
    {
        public belief()
        {
            this.partition = DbContext.InitialContext;
        }

        public string partition { get; set; }
        public new string variable { get; set; }
        public character character { get; set; }
        public virtual List<hypothesis> hypotheses { get; set; }
        public virtual List<belief> parents { get { return this._parents; } set { this._parents = value; } }
        public virtual List<belief> children { get { return this._children; } set { this._children = value; } }
    }

    public class hypothesis : IHasIntId, IHasPartitionString
    {
        [Key]
        public int id { get; set; }
        public string partition { get; set; }
        public string name { get; set; }
        public belief belief { get; set; }
        public virtual List<proposition> propositions { get; set; }
    }

    public class proposition : IHasIntId, IHasPartitionString
    {
        [Key]
        public int id { get; set; }
        public string partition { get; set; }
        public hypothesis hypothesis { get; set; }
        public string name { get; set; }
        public decimal value { get; set; }
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

    public abstract class element : Entity, IHasIntId, IHasPartitionString
    {
        public element()
        {
            this.partition = DbContext.InitialContext;
        }

        [Key]
        public override int id { get; set; }
        public string partition { get; set; }
        public string gm_name { get; set; }
        public string description { get; set; }
        public virtual List<creation_process> creation_history { get; set; }

        [NotMapped]
        public override string variable
        {
            get { return this.gm_name; }
            set { this.gm_name = value; }
        }
    }
}

