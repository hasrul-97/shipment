using Dapper;
using ExceptionHandler.Abstraction.DataAccess;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler.DataAccess
{
    public class Repository : IRepository
    {
        private static string _connectionString = string.Empty;

        public Repository(IConfiguration configuration)
        {
            string host = configuration["ConnectionString:Server"];
            string port = configuration["ConnectionString:Port"];
            string database = configuration["ConnectionString:Database"];
            string uid = configuration["ConnectionString:Uid"];
            string password = configuration["ConnectionString:Password"];

            _connectionString = string.Format(configuration["ConnectionString:URL"], host, port, database, uid, password);
        }
        public async Task<int> AddToDatabase(string sqlQuery)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    int numberOfRowsAffected = await _sqlConnection.ExecuteAsync(sqlQuery);
                    return numberOfRowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<T> FetchData<T>(string sqlQuery)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    var result = (await _sqlConnection.QueryAsync<T>(sqlQuery)).ToList();
                    if (result.Count > 0)
                    {
                        return result[0];
                    }
                    else
                    {
                        return default;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<T> FetchDataWithParameter<T>(string sqlQuery, object parameter)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    var result = (await _sqlConnection.QueryAsync<T>(sqlQuery, parameter)).ToList();
                    if (result.Count > 0)
                    {
                        return result[0];
                    }
                    else
                    {
                        return default;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<int> AddToDatabaseWithParameter(string sqlQuery, object parameter)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    int numberOfRowsAffected = await _sqlConnection.ExecuteAsync(sqlQuery, parameter);
                    return numberOfRowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<int> Delete(string sqlQuery)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    int numberOfRowsAffected = await _sqlConnection.ExecuteAsync(sqlQuery);
                    return numberOfRowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<int> DeleteWithParameter(string sqlQuery, object parameter)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    int numberOfRowsAffected = await _sqlConnection.ExecuteAsync(sqlQuery, parameter);
                    return numberOfRowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<List<T>> FetchList<T>(string sqlQuery)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    List<T> result = (await _sqlConnection.QueryAsync<T>(sqlQuery)).ToList();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<List<T>> FetchListWithParameter<T>(string sqlQuery, object parameter)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    List<T> result = (await _sqlConnection.QueryAsync<T>(sqlQuery, parameter)).ToList();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<int> Update(string sqlQuery)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    int numberOfRowsAffected = await _sqlConnection.ExecuteAsync(sqlQuery);
                    return numberOfRowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<int> UpdateWithParameter(string sqlQuery, object parameter)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    int numberOfRowsAffected = await _sqlConnection.ExecuteAsync(sqlQuery, parameter);
                    return numberOfRowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public async Task<int> AddToDatabaseBulkWithParameter<T>(string sqlQuery, List<T> parameter)
        {
            using (MySqlConnection _sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    int numberOfRowsAffected = await _sqlConnection.ExecuteAsync(sqlQuery, parameter);
                    return numberOfRowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }
    }
}
