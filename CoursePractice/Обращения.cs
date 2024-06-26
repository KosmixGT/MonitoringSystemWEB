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
    using System.ComponentModel.DataAnnotations;

    public partial class Обращения
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Обращения()
        {
            this.Фото = new HashSet<Фото>();
        }
        [Display(Name = "Номер обращения")]
        public int код_обращения { get; set; }
        public int тема { get; set; }
        public int сезонность { get; set; }
        public Nullable<byte> срок_подг_ответа { get; set; }
        public Nullable<byte> срок_подтв_ответа { get; set; }
        public string описание { get; set; }
        public int статус_обращения { get; set; }
        [Display(Name = "Дата обращения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime дата_обращения { get; set; }
        public int объект_МКД { get; set; }
        public string обслужив_лицо { get; set; }
        public string оценка { get; set; }
        public Nullable<int> учётная_запись { get; set; }
    
        public virtual МКД МКД { get; set; }
        public virtual Сезон_обращения Сезон_обращения { get; set; }
        public virtual Статус_обращения Статус_обращения1 { get; set; }
        public virtual Тема_обращения Тема_обращения { get; set; }
        public virtual Учётные_записи Учётные_записи { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Фото> Фото { get; set; }
    }
}
