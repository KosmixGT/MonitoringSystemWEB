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
    
    public partial class Фото
    {
        
        public int код_фото { get; set; }
        public int номер_обращения { get; set; }
        public byte[] данные_фото { get; set; }
    
        public virtual Обращения Обращения { get; set; }
    }
}
