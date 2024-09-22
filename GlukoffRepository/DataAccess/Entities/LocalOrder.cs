using System.ComponentModel.DataAnnotations.Schema;

namespace GlukoffRepository.DataAccess;
[Table ("Catalog")]
public class LocalOrder 
{
    [Column("id")]
    public int Id { get; set; }
    [Column("Data_priema")]
    public string DateOrder { get; set; }
    [Column("WhatRemont")]
    public string Tittle { get; set; } 
    [Column("Status_remonta")]
    public string Status { get; set; }

}

