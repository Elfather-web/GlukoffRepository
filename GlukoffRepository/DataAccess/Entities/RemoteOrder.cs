using System.ComponentModel.DataAnnotations.Schema;

namespace GlukoffRepository.DataAccess;
[Table ("fdT234Tf_statusoforders")]
public record RemoteOrder([property: Column("orderid")] int Id, 
    [property: Column("ordertittle")] string Tittle,
    [property: Column("orderstatus")] string Status,
    [property: Column("orderdate")] DateTime DateOrder);

    /*[Column("orderid")]
    public int Id { get; set; }
    [Column("orderdate")]
    public DateTime DateOfOrder { get; set; }
    [Column("ordertittle")]
    public string Tittle { get; set; } 
    [Column("orderstatus")]
    public string Status { get; set; }*/

