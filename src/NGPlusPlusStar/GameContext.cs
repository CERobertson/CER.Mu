namespace NGPlusPlusStar
{
    using rpg = CER.Rpg;
    using System;
    using System.Data.Entity;

    public class GameContext : DbContext
    {
        public string CurrentGame { get; private set; }
        public string Partition { get { return this.CurrentGame; } }

        public GameContext()
        {
            this.NewGame();
        }
        public string NewGame()
        {
            var partition = Guid.NewGuid().ToString().Replace("-", string.Empty).Insert(0, "_");
            this.CurrentGame = partition;
            return partition;
        }

        public GameContext(string partition)
        {
            this.LoadGame(partition);
        }
        public void LoadGame(string partition)
        {
            this.CurrentGame = partition;
        }

        //public T CreateOrRetrieve<T>(DbSet<T> set, T obj) where T : class, IHasIntId, IHasPartitionString
        //{
        //    obj.partition = this.CurrentGame;
        //    return base.CreateOrRetrieve(set, x => x.id == obj.id && x.partition == obj.partition, obj);
        //}

        //public T CreateOrRetrieve<T>(DbSet<T> set, T obj, params Func<T, T, bool> predicates)
        //{

        //}
        public DbSet<rpg.belief> B1 { get; set; }
        public DbSet<rpg.belief> B2 { get; set; }
    }
}
