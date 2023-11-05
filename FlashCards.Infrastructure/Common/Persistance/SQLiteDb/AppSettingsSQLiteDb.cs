using Microsoft.Extensions.Configuration;

namespace FlashCards.Infrastructure.Common.Persistance.SQLiteDb
{
    public class AppSettingsSQLiteDb
    {
        private readonly IConfiguration _configuration;

        public AppSettingsSQLiteDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SQLite.SQLiteOpenFlags Flags => SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;
        public string SQLiteDatabase => _configuration["ConnectionStrings:SQLiteDatabase"] + ".db3";
        public string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, SQLiteDatabase);
    }
}
