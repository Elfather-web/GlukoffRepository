using GlukoffRepository.DataAccess;
using Org.BouncyCastle.Math;
using BigInteger = System.Numerics.BigInteger;

namespace DataAcces;

public record BuffOrder(long Id, string Tittle, string Status, DateTime DateOrder, long Barcode)
{
    public static BuffOrder? FromLocal(LocalOrder localOrder)
    {
        localOrder = localOrder with { Title = localOrder.Title + " " + localOrder.Brand + " " + localOrder.Model };
            
              return  Int64.TryParse(localOrder.Barcode, out var barcode)?
                      DateTime.TryParse(localOrder.DateOrder, out var dateTime)
                      ? new BuffOrder(localOrder.Id, localOrder.Title, localOrder.Status, dateTime, barcode)
                      : null
                          :null;
    }

    public static BuffOrder FromRemote(RemoteOrder remoteOrder)
    {
       return new BuffOrder(remoteOrder.Id, remoteOrder.Title, remoteOrder.Status, remoteOrder.DateOrder, remoteOrder.Barcode);
    }
}