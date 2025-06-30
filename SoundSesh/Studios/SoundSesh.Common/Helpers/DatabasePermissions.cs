using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading.Tasks;

namespace SoundSesh.Common.Services
{
    public static class DatabasePermissions
    {
        public static async Task RecreateDatabaseWithIISPermissions(DbContext context, string dbName, string applicationPoolName, ILogger logger)
        {
            await CreateServerLogin(context, applicationPoolName, dbName, logger);
            await CreateDbLogin(context, dbName, applicationPoolName, logger);
            await CreateDbPermission(context, dbName, applicationPoolName, "reader", logger);
            await CreateDbPermission(context, dbName, applicationPoolName, "writer", logger);
            return;
        }

        private static async Task CreateServerLogin(DbContext context, string applicationPoolName, string dbName, ILogger logger)
        {
            await context.Database.ExecuteSqlCommandAsync(CreateServerLoginScript(applicationPoolName, dbName));
        }

        private static async Task CreateDbLogin(DbContext context, string dbName, string applicationPoolName, ILogger logger)
        {
            await context.Database.ExecuteSqlCommandAsync(CreateDbLoginScript(dbName, applicationPoolName));
        }

        private static async Task CreateDbPermission(DbContext context, string dbName, string applicationPoolName, string permission, ILogger logger)
        {
            await context.Database.ExecuteSqlCommandAsync(CreateDbPermission(dbName, applicationPoolName, permission));
        }

        private static string CreateServerLoginScript(string applicationPoolName, string dbName)
        {
            return $@"
                    USE Master
                    IF NOT EXISTS (SELECT loginname FROM master.dbo.syslogins WHERE NAME = 'IIS APPPOOL\{applicationPoolName}')
                    BEGIN
                        CREATE LOGIN [IIS APPPOOL\{applicationPoolName}] FROM WINDOWS WITH DEFAULT_DATABASE=[{dbName}], DEFAULT_LANGUAGE=[us_english]
                    END";
        }

        private static string CreateDbLoginScript(string dbName, string applicationPoolName)
        {
            return $@"
                    USE [{dbName}]
                    IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE NAME = 'IIS APPPOOL\{applicationPoolName}')
                    BEGIN
                        CREATE USER [IIS APPPOOL\{applicationPoolName}] FROM LOGIN [IIS APPPOOL\{applicationPoolName}]
                    END";
        }

        private static string CreateDbPermission(string dbName, string applicationPoolName, string permission)
        {
            return $@"USE [{dbName}] EXECUTE sp_addrolemember 'db_data{permission}', 'IIS APPPOOL\{applicationPoolName}'";
        }
    }
}
