using Core.DataAccess;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        // Autofac bu sınıftan ilk nesneyi üretirken tam 1 kez çalışır
        // Dapper Snake_Case eşleme ayarı (Buraya ekleyin)
        static NpgsqlConnectionFactory()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public NpgsqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgreSql")!;
        }

        public IDbConnection CreateConnection()
        {
            var connection=new NpgsqlConnection(_connectionString);
            return connection;
        }
    }
}
