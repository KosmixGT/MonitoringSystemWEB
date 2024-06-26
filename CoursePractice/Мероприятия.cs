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
    using Microsoft.Office.Interop.Excel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Мероприятия
    {
        public int код_мероприятия { get; set; }
        public int кат_меропр { get; set; }
        public string тема_меропр { get; set; }
        public int объект_МКД { get; set; }
        public string описание { get; set; }
        [Display(Name = "Дата и время проведения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public System.DateTime дата_время_проведения { get; set; }
        public string исполнитель { get; set; }
        public int статус_меропр { get; set; }
    
        public virtual Категория_мероприятия Категория_мероприятия { get; set; }
        public virtual МКД МКД { get; set; }
        public virtual Статус_мероприятия Статус_мероприятия { get; set; }
    }
}
