using System.ComponentModel.DataAnnotations.Schema;

namespace GlukoffRepository.DataAccess;

[Table("Catalog")]
public record LocalOrder(
    [property: Column("id")] long Id,
    [property: Column("Data_priema")] string DateOrder,
    [property: Column("WhatRemont")] string Tittle,
    [property: Column("Status_remonta")] string Status);

