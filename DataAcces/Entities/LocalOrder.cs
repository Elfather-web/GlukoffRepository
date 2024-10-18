using System.ComponentModel.DataAnnotations.Schema;

namespace GlukoffRepository.DataAccess;

[Table("Catalog")]
public record LocalOrder(
    [property: Column("id")] long Id,
    [property: Column("Data_priema")] string DateOrder,
    [property: Column("WhatRemont")] string Title,
    [property: Column("Status_remonta")] string Status,
    [property: Column("Barcode")] string Barcode,
    [property: Column("brand")] string Brand,
    [property: Column("model")] string Model);

