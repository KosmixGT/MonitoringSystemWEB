//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoursePractice
{
    using System;
    using System.Collections.Generic;
    
    public partial class Тип_учётной_записи
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Тип_учётной_записи()
        {
            this.Учётные_записи = new HashSet<Учётные_записи>();
        }
    
        public int код_типа_пользователя { get; set; }
        public string тип_пользователя { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Учётные_записи> Учётные_записи { get; set; }
    }
}
