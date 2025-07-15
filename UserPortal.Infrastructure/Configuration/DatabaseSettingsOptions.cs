namespace UserPortal.Infrastructure.Configuration
{
    /// <summary>
    /// Class representing the database settings options in appsetting.js
    /// SqlServerConnectionString is used to connect to the SQL Server database.Likewaise, 
    /// other database connections can be added here.
    /// </summary>
    public class DatabaseSettingsOptions
    {
        public const string SectionName = "DatabaseSettings";
        public string SqlServerConnectionString { get; set; } = null!;
    }
}
