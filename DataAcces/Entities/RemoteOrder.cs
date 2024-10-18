using System.ComponentModel.DataAnnotations.Schema;

namespace GlukoffRepository.DataAccess;

[Table("fdT234Tf_statusoforders")]
public record RemoteOrder(
    [property: Column("orderid")] int Id,
    [property: Column("ordertitle")] string Title,
    [property: Column("orderstatus")] string Status,
    [property: Column("orderdate")] DateTime DateOrder, 
    [property: Column("Barcode")] long Barcode);