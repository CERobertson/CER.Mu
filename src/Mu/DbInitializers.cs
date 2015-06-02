namespace CER.Mu
{
    using CER.Rpg;
    using ef = System.Data.Entity;

    public class DropCreateSeedDatabaseAlways : ef.DropCreateDatabaseAlways<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            DbSeeds.Seed_SourceCompiled(context);
            base.Seed(context);
        }
    }
    public class CreateSeedDatabaseIfNotExists : ef.CreateDatabaseIfNotExists<DbContext> 
    {
        protected override void Seed(DbContext context)
        {
            DbSeeds.Seed_SourceCompiled(context);
            base.Seed(context);
        }
    }
}
