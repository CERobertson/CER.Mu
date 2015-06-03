namespace CER.ng
{
    using CER.Rpg;
    using System;

    public class GameContext : DbContext
    {
        public string CurrentGame { get; private set; }
        public override string Partition { get { return this.CurrentGame; } }

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

        public override T CreateOrRetrieve<T>(System.Data.Entity.DbSet<T> set, System.Func<T, bool> predicate, T obj, bool SaveOnCreate = true)
        {
            obj.partition = this.CurrentGame;
            return base.CreateOrRetrieve<T>(set, predicate, obj, SaveOnCreate);
        }
    }
}
