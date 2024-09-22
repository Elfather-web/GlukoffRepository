using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Net.Mime;
using System.Reflection;
using Dapper;
using GlukoffRepository.DataAccess;
using GlukoffRepository.Services;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Data.Sqlite;
using MySqlX.XDevAPI.Relational;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;

namespace GlukoffRepository.Abstraction;

public abstract class SqliteRepository<TEntity> : IRepository<TEntity>
{
    private readonly string _connection;

    public SqliteRepository(string connection)
    {
        _connection = connection;
    }

    public Task<TEntity> SelectAsync(int id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> SelectAsync(CancellationToken token)
    {
        //Подключаемся к БД, скачиваем. Отключаемся.
        using var connection = new SqliteConnection(_connection);
        connection.Open();

        var tableAttribute = typeof(TEntity)
            .GetCustomAttributes(typeof(TableAttribute), true)
            .FirstOrDefault() as TableAttribute;

        var tableName =
            tableAttribute is not null ? tableAttribute.Name : typeof(TEntity).Name;
        //Получить публичные свойства из т энтити, с помощью рефлексии.
        //Для каждого свойства получить аттрибут колумн, если его нет то имя. 

        var normalisedNames = GetNormalisedPropertyNames<TEntity>();
        var sqlExpression = $"SELECT {normalisedNames}  FROM {tableName}";
        return connection.QueryAsync<TEntity>(sqlExpression);
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