using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Net.Mime;
using System.Reflection;
using Dapper;
using GlukoffRepository.DataAccess;
using GlukoffRepository.Services;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;

namespace GlukoffRepository.Abstraction;

public abstract class MySqlRepository <TEntity> : IRepository <TEntity>
{
    private readonly string _connection;
    public MySqlRepository(string connection)
    {
        _connection = connection;
    }

    public async Task<TEntity> SelectAsync(int id, CancellationToken token)
    {
        using var connection = new MySqlConnection(_connection);
        connection.Open();

        var tableAttribute = typeof(TEntity)
            .GetCustomAttributes(typeof(TableAttribute), true)
            .FirstOrDefault() as TableAttribute;
        var tableName =
            tableAttribute is not null ? tableAttribute.Name : typeof(TEntity).Name;
        var normalisedNames = GetNormalisedPropertyNames<TEntity>();
        var sqlExpression = $"SELECT {normalisedNames} FROM {tableName} where orderid={id}";
        var order = connection.QueryFirst<TEntity>(sqlExpression);
        return order;

    }

    public Task<List<TEntity>> SelectAsyncRows(CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> SelectAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(TEntity entity, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    private static string GetNormalisedPropertyNames<TEntity>()
    {
        var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var normalisedNames = new List <string>();
        foreach (var p in properties)
        {
            var columnAttribute =
                p.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() as ColumnAttribute;
            var columnName =
                columnAttribute is not null ? columnAttribute.Name : p.Name;
            normalisedNames .Add($"{columnName} as {p.Name}");
        }
        return string.Join(',', normalisedNames);
    }
}