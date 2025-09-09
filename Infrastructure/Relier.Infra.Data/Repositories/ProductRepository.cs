using Microsoft.Data.SqlClient;
using Relier.Domain.Entities;
using Relier.Domain.Interfaces;
using System.Data;

namespace Relier.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly SqlConnection db;
        public ProductRepository(string connectionString)
        {
            db = new SqlConnection(connectionString);
        }
        public async Task<Product?> Create(Product product)
        {
            string query = @"INSERT INTO dbo.Products(Name,Description,Stock,Price,Image,CategoryId) OUTPUT INSERTED.Id
                              VALUES (@Name,@Description,@Stock,@Price,@Image,@CategoryId)";
            try
            {
                await db.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, db))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Stock", product.Stock);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Image", product.Image);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                    product.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
            finally
            {
                db.Close();
            }

            return product;
        }

        public async Task Delete(Product product)
        {
            var query = "DELETE FROM[dbo].[Products] WHERE Id = @Id";
            try
            {
                await db.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, db))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", product.Id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            finally
            {
                await db.CloseAsync();
            }
            await Task.CompletedTask;
        }

        public async Task<Product> update(Product product)
        {
            var query = @"UPDATE [dbo].[Products] Set Name = @Name,Description = @Description,Stock = @Stock,Price = @Price,Image = @Image,CategoryId = @CategoryId Where Id = @Id";
            try
            {
                await db.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, db))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Stock", product.Stock);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Image", product.Image);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    cmd.Parameters.AddWithValue("@Id", product.Id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            finally
            {
                await db.CloseAsync();
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            List<Product> response = new List<Product>();
            var query = string.Format("SELECT Id,Name,Description,Stock,Price,Image,CategoryId FROM [dbo].[Products]");
            try
            {
                await db.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, db);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (await reader.ReadAsync())
                    {
                        response.Add(Load(reader));
                    }
                }
            }
            finally
            {
                await db.CloseAsync();
            }
            return response;
        }

        public async Task<Product?> GetById(int id)
        {
            Product? response = null;
            var query = string.Format("SELECT Id,Name,Description,Stock,Price,Image,CategoryId FROM [dbo].[Products] WHERE Id = @Id");
            try
            {
                await db.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    if (await reader.ReadAsync())
                    {
                        response = Load(reader);
                    }
                }
            }
            finally
            {
                await db.CloseAsync();
            }
            return response;
        }

        private Product Load(IDataReader reader)
        {
            Product response = new Product();
            response.Category = new Category();
            if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                response.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            if (!reader.IsDBNull(reader.GetOrdinal("Name")))
                response.Name = reader.GetString(reader.GetOrdinal("Name"));
            if (!reader.IsDBNull(reader.GetOrdinal("Description")))
                response.Description = reader.GetString(reader.GetOrdinal("Description"));
            if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                response.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
            if (!reader.IsDBNull(reader.GetOrdinal("Stock")))
                response.Stock = reader.GetInt32(reader.GetOrdinal("Stock"));
            if (!reader.IsDBNull(reader.GetOrdinal("Image")))
                response.Image = reader.GetString(reader.GetOrdinal("Image"));
            if (!reader.IsDBNull(reader.GetOrdinal("CategoryId")))
                response.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
            //if (!reader.IsDBNull(reader.GetOrdinal("CategoryNome")))
            //    response.Category.Name = reader.GetString(reader.GetOrdinal("CategoryName"));
            //response.Category.Id = response.CategoryId;
            return response;
        }
    }
}
