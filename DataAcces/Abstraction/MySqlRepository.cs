using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper;
using MySql.Data.MySqlClient;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;

namespace GlukoffRepository.Abstraction;

public abstract class MySqlRepository<TEntity> : IRepository<TEntity>
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

    public async Task<List<TEntity>> SelectAsyncRows(CancellationToken token)
    {
        using var connection = new MySqlConnection(_connection);
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
        using var connection = new MySqlConnection(_connection);
        await connection.ExecuteAsync(
            "INSERT INTO fdT234Tf_statusoforders (orderid, ordertitle, orderstatus, orderdate, barcode) " +
            "VALUES (@Id, @Title, @Status, @DateOrder, @Barcode)", entity);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken token)
    {
        using var connection = new MySqlConnection(_connection);
        await connection.ExecuteAsync("UPDATE fdT234Tf_statusoforders set  " +
                                      "orderdate = @DateOrder, " +
                                      "ordertitle = @Title, " +
                                      "orderstatus = @Status, Barcode = @Barcode " +
                                      "where orderid=@Id", entity);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken token)
    {
        using var connection = new MySqlConnection(_connection);
        await connection.ExecuteAsync("DELETE FROM fdT234Tf_statusoforders WHERE orderid=@Id", entity);
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