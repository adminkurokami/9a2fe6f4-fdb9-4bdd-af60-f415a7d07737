using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using TestFirebird.Model;

namespace TestFirebird.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbConnection dbConnection, bool contextOwnsConnection) : base(dbConnection, contextOwnsConnection)
        {

        }
        public ApplicationDbContext(string connectionString) : base(connectionString)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<University> Universities { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }


        public class MyContextFactory : IDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext Create()
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;

                //Temporary fix for VS blocking FB assembly
                Guid guid = Guid.NewGuid();
                string fixPath = path + "fbembed_" + guid + ".dll";
                File.Copy(path + "fbembed.dll", fixPath);

                FbConnectionStringBuilder csb = new FbConnectionStringBuilder();
                csb.ServerType = FbServerType.Embedded;
                csb.UserID = "SYSDBA";
                csb.Password = "masterkey";
                csb.Dialect = 3;
                csb.Database = path + "MYDB.DB";
                csb.ClientLibrary = fixPath;

                return new ApplicationDbContext(csb.ConnectionString);
            }
        }
    }
}
