using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Stock : Entity
{
    [Key] public Guid ProductId { get; set; }
    [Key] public Guid StoreId { get; set; }
    public int Quantity { get; set; }
}