namespace CER.Mu
{
    using CER.Graphs.SetExtensions;
    using CER.Rpg;
    using System.Linq;

    public static class DbSeeds
    {
        public static void Seed_SourceCompiled(DbContext rpg)
        {
            foreach (var creation_proccesses in typeof(CreationProcesses)._Fields<CreationProcesses>().SelectMany(x => x._Fields<creation_process>()))
            {
                rpg.CreationProcesses.Add(creation_proccesses);
            }
            var games = new Games();
            rpg.Games.Add(games.Mu);
            rpg.Games.Add(games.OutsideTime);
            rpg.Plots.Add(games.Plots.Witness_Prologue);
            rpg.SaveChanges();

        }
    }
}
