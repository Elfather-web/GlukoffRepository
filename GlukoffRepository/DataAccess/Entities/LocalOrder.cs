using System.ComponentModel.DataAnnotations.Schema;

namespace GlukoffRepository.DataAccess;

[Table("Catalog")]
public record LocalOrder([property: Column("id")] long Id, 
  [property: Column("Data_priema")] string DateOrder,
  [property: Column("WhatRemont")] string Tittle,
  [property: Column("Status_remonta")] string Status);

    // [Column("id")] public int Id { get; set; }
    // [Column("Data_priema")] public string DateOrder { get; set; }
    // [Column("WhatRemont")] public string Tittle { get; set; }
    // [Column("Status_remonta")] public string Status { get; set; }
