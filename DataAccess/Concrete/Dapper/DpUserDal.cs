using Core.DataAccess;
using Core.Entities.Concrete;
using Dapper;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper
{
    public class DpUserDal : IUserDal
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DpUserDal(IDbConnectionFactory dbConnectionFactory)
        {
            _connectionFactory = dbConnectionFactory;
        }

        public async Task<Guid> AddAsync(User entity)
        {
            const string sql = @"
            INSERT INTO users (first_name, last_name, email, password_salt, password_hash, status)
            VALUES (@FirstName, @LastName, @Email, @PasswordSalt, @PasswordHash, @Status)
            RETURNING id;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<Guid>(sql, entity);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            const string sql = @"
                        SELECT 
                            id AS Id, 
                            first_name AS FirstName, 
                            last_name AS LastName, 
                            email AS Email, 
                            password_salt AS PasswordSalt, 
                            password_hash AS PasswordHash, 
                            status AS Status 
                        FROM users 
                        WHERE id = @Id";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<User?> GetByMailAsync(string mail)
        {
            const string sql = @"
                    SELECT 
                        id AS Id, 
                        first_name AS FirstName, 
                        last_name AS LastName, 
                        email AS Email, 
                        password_salt AS PasswordSalt, 
                        password_hash AS PasswordHash, 
                        status AS Status 
                    FROM users 
                    WHERE email = @Email";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = mail });
        
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            const string sql = @"
                            SELECT 
                                oc.id AS Id, 
                                oc.name AS Name 
                            FROM operation_claims oc
                            INNER JOIN user_operation_claims uoc ON uoc.operation_claim_id = oc.id
                            WHERE uoc.user_id = @UserId";

            using var connection = _connectionFactory.CreateConnection();
            var claims = await connection.QueryAsync<OperationClaim>(sql, new { UserId = user.Id });
            return claims.ToList();
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            const string sql = @"
                        UPDATE users 
                        SET 
                            first_name = @FirstName,
                            last_name = @LastName,
                            email = @Email,
                            password_salt = @PasswordSalt,
                            password_hash = @PasswordHash,
                            status = @Status
                        WHERE id = @Id";

            using var connection = _connectionFactory.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, entity);

            return rowsAffected > 0;
        }
    }
}
