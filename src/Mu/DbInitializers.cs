namespace CER.Mu
{
    using CER.Graphs.SetExtensions;
    using CER.Rpg;
    using ef = System.Data.Entity;
    using System.Linq;

    public class DropCreateDbInitializer : ef.DropCreateDatabaseAlways<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            foreach (var creation_proccesses in typeof(CreationProcesses)._Fields<CreationProcesses>().SelectMany(x => x._Fields<creation_process>()))
            {
                context.CreationProcesses.Add(creation_proccesses);
            }
            var games = new Games();
            context.Games.Add(games.Mu);
            context.Games.Add(games.OutsideTime);
            context.Plots.Add(games.Plots.Witness_Prologue);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
