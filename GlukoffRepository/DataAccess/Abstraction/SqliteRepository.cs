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

    public async Task<TEntity> SelectAsync(int orderId, CancellationToken token)
    {
        using var connection = new SqliteConnection(_connection);
        connection.Open();

        var tableAttribute = typeof(TEntity)
            .GetCustomAttributes(typeof(TableAttribute), true)
            .FirstOrDefault() as TableAttribute;

        var tableName =
            tableAttribute is not null ? tableAttribute.Name : typeof(TEntity).Name;
        var normalisedNames = GetNormalisedPropertyNames<TEntity>();
        var sqlExpression = $"SELECT {normalisedNames} FROM {tableName} where id={orderId}";
        var order = connection.QueryFirst<TEntity>(sqlExpression);
        return order;
    }

    public async Task<List<TEntity>> SelectAsyncRows(CancellationToken token)
    {
        //Подключаемся к БД, скачиваем.
        using var connection = new SqliteConnection(_connection);
        connection.Open();

        var tableAttribute = typeof(TEntity)
            .GetCustomAttributes(typeof(TableAttribute), true)
            .FirstOrDefault() as TableAttribute;

        var tableName =
            tableAttribute is not null ? tableAttribute.Name : typeof(TEntity).Name;
         

        var normalisedNames = GetNormalisedPropertyNames<TEntity>();
        var sqlExpression = $"SELECT {normalisedNames} FROM {tableName}";
        var rows = await connection.QueryAsync<TEntity>(sqlExpression);
        return rows.ToList();
    }

    public async Task InsertAsync(TEntity entity, CancellationToken token)
    {
        using var connection = new SqliteConnection(_connection);
        await connection.ExecuteAsync("INSERT INTO Catalog (id, Data_priema, WhatRemont, Status_remonta) " +
                                      "VALUES (@Id, @DateOrder, @Tittle, @Status)", entity);
    }
    
    public async Task UpdateAsync(TEntity entity, CancellationToken token)
    {
        using var connection = new SqliteConnection(_connection);
        await connection.ExecuteAsync("UPDATE Catalog set  " +
                                      "Data_priema = @DateOrder, " +
                                      "WhatRemont = @Tittle, " +
                                      "Status_remonta = @Status where id=@Id", entity);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken token)
    {
        using var connection = new SqliteConnection(_connection);
        await connection.ExecuteAsync("DELETE FROM Catalog WHERE id=@Id", entity);
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