namespace CER.ng
{
    using CER.Rpg;
    using System;

    public class GameContext : DbContext
    {
        public string CurrentGame { get; private set; }

        public GameContext()
        {
            this.NewGame();
        }
        public string NewGame()
        {
            var partition = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
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

        public override T CreaetOrRetrieve<T>(System.Data.Entity.DbSet<T> set, System.Func<T, bool> predicate, T obj, bool SaveOnCreate = true)
        {
            obj.partition = this.CurrentGame;
            return base.CreaetOrRetrieve<T>(set, predicate, obj, SaveOnCreate);
        }
    }
}
