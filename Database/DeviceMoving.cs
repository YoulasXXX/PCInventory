//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PCInventory.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeviceMoving
    {
        public int DeviceMovingID { get; set; }
        public System.DateTime DeviceMovingDate { get; set; }
        public int UserID { get; set; }
        public int DeviceID { get; set; }
        public int WorkplaceID { get; set; }
    
        public virtual Device Device { get; set; }
        public virtual User User { get; set; }
        public virtual Workplace Workplace { get; set; }
    }
}