﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper;
using Microsoft.Data.Sqlite;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;

namespace GlukoffRepository.Abstraction;

public abstract class SqliteRepository<TEntity> : IRepository<TEntity>
{
    private readonly string _connection;

    public SqliteRepository(string connection)
    {
        _connection = connection;
    }

    public async Task<TEntity> SelectAsync(int id, CancellationToken token)
    {
        using var connection = new SqliteConnection(_connection);
        connection.Open();

        var tableAttribute = typeof(TEntity)
            .GetCustomAttributes(typeof(TableAttribute), true)
            .FirstOrDefault() as TableAttribute;

        var tableName =
            tableAttribute is not null ? tableAttribute.Name : typeof(TEntity).Name;
        var normalisedNames = GetNormalisedPropertyNames<TEntity>();
        var sqlExpression = $"SELECT {normalisedNames} FROM {tableName} where id={id}";
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
        await connection.ExecuteAsync("INSERT INTO Catalog (id, Data_priema, WhatRemont, Status_remonta, Barcode, Brand, Model) " +
                                      "VALUES (@Id, @DateOrder, @Title, @Status, @Barcode, @brand, @model)", entity);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken token)
    {
        using var connection = new SqliteConnection(_connection);
        await connection.ExecuteAsync("UPDATE Catalog set  " +
                                      "Data_priema = @DateOrder, " +
                                      "WhatRemont = @Title, " +
                                      "Status_remonta = @Status, " +
                                      "Barcode = @Barcode, " +
                                      "Brand = @brand, " +
                                      "Model = @model where id=@Id", entity);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken token)
    {
        using var connection = new SqliteConnection(_connection);
        await connection.ExecuteAsync("DELETE FROM Catalog WHERE id=@Id", entity);
    }


    private static string GetNormalisedPropertyNames<TEntity>()
    {
        var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var normalisedNames = new List<string>();
        foreach (var p in properties)
        {
            var columnAttribute =
                p.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() as ColumnAttribute;
            var columnName =
                columnAttribute is not null ? columnAttribute.Name : p.Name;
            normalisedNames.Add($"{columnName} as {p.Name}");
        }

        return string.Join(',', normalisedNames);
    }
}