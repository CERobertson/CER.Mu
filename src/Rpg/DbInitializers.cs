namespace CER.Rpg
{
    using CER.Graphs.SetExtensions;
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
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
