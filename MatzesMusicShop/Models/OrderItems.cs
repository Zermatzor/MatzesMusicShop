//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MatzesMusicShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderItems
    {
        public int Id { get; set; }
        public Nullable<int> OrderId { get; set; }
        public int CDId { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual CDs CDs { get; set; }
        public virtual Orders Orders { get; set; }
    }
}
