using System.Threading.Tasks;
using GADev.BarberPoint.Application.Repositories.Core;
using Microsoft.Extensions.Configuration;
using Dommel;
using System;
using MySql.Data.MySqlClient;

namespace GADev.BarberPoint.Infrastructure.Repositories.Core
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Domain.Entities.Core.BaseEntity
    {
        private readonly string _connectionString;

        public GenericRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            RegisterMappings.Register();
        }

        public async Task<T> GetAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var result = await connection.GetAsync<T>(id);

                return result;
            }
        }

        public async Task<int?> CreateAsync(T entity)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var result = (await connection.InsertAsync(entity)).ToString();

                int id = int.Parse(result);

                if (id > 0)
                {
                    return id;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Error insert type {0} in the database", entity.GetType().Name));
                }
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var result = await connection.UpdateAsync(entity);

                if (result)
                {
                    return result;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Error update type {0} in the database", entity.GetType().Name));
                }
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var result = await connection.DeleteAsync(entity);

                if (result)
                {
                    return result;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Error delete type {0} in the database", entity.GetType().Name));
                }
            }
        }
    }
}