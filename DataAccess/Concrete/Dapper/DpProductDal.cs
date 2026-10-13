using Core.DataAccess;
using Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper
{
    public class DpProductDal : IProductDal
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DpProductDal(IDbConnectionFactory dbConnectionFactory)
        {
            _connectionFactory = dbConnectionFactory;
        }

        public async Task<Guid> AddAsync(Product entity)
        {
            const string sql = @"
            INSERT INTO products (product_name, category_id, unit_price, units_in_stock)
            VALUES (@ProductName, @CategoryId, @UnitPrice, @UnitsInStock)
            RETURNING id;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<Guid>(sql, entity);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            const string sql = "SELECT * FROM products WHERE is_active = true;";
            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<Product>(sql);
            return result.AsList();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM products WHERE id = @Id;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<List<ProductDetailDto>> GetProductDetailsAsync()
        {
            const string sql = @"
            SELECT p.id AS ProductId, p.product_name AS ProductName, 
                   c.category_name AS CategoryName, p.units_in_stock AS UnitsInStock
            FROM products p
            INNER JOIN categories c ON p.category_id = c.id;";

            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<ProductDetailDto>(sql);
            return result.AsList();
        }

        public Task<bool> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
