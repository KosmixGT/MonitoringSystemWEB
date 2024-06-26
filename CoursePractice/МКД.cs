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

    public partial class МКД
    {
        public МКД()
        {
            this.Мероприятия = new HashSet<Мероприятия>();
            this.Обращения = new HashSet<Обращения>();
        }
    
        public int код_МКД { get; set; }
        [Display(Name = "Адрес")]
        public string адрес { get; set; }
        [Display(Name = "Количество жилых помещений (квартир)")]
        public Nullable<short> кол_жил_пом { get; set; }
        [Display(Name = "Состояние дома")]
        public int состояние_МКД { get; set; }
        [Display(Name = "Количество детских площадок")]
        public string детская_площадка { get; set; }
        [Display(Name = "Количество спортивных площадок")]
        public string спортивная_площадка { get; set; }
        [Display(Name = "Тип управления")]
        public int тип_управления { get; set; }
        [Display(Name = "ОКТМО")]
        public string ОКТМО { get; set; }
        [Display(Name = "Управляющая компания")]
        public Nullable<int> УК { get; set; }
        [Display(Name = "Кадастровый номер")]
        public string кадастровый_номер { get; set; }
        [Display(Name = "Год постройки")]
        public string год_постройки { get; set; }
        [Display(Name = "Год ввода дома в эксплуатацию")]
        public string год_ВДВУ { get; set; }
        [Display(Name = "Тип дома")]
        public string тип_дома { get; set; }
        [Display(Name = "Количество этажей")]
        public string кол_этажей { get; set; }
        [Display(Name = "Количество подеъездов")]
        public string кол_подъездов { get; set; }
        [Display(Name = "Количество лифтов")]
        public string кол_лифтов { get; set; }
        [Display(Name = "Площадь жилых помещений")]
        public Nullable<decimal> площадь_жил_пом { get; set; }
        [Display(Name = "Площадь нежилых помещений")]
        public Nullable<decimal> площадь_нежил_пом { get; set; }
        [Display(Name = "Площадь входящая в состав общего имущества")]
        public Nullable<decimal> площадь_ПВСОИ { get; set; }
        [Display(Name = "Площадь земельного участка")]
        public Nullable<decimal> площадь_зем_участка { get; set; }
        [Display(Name = "Тип фундамента")]
        public string фундамент { get; set; }
        [Display(Name = "Тип перекрытий")]
        public string тип_перекрытий { get; set; }
        [Display(Name = "Материал несущих стен")]
        public string мат_несущих_стен { get; set; }
        [Display(Name = "Площадь подвала")]
        public Nullable<decimal> площадь_подвала { get; set; }
        [Display(Name = "Тип мусоропровода")]
        public string тип_мусоропровода { get; set; }
        [Display(Name = "Вентиляция")]
        public string вентиляция { get; set; }
        [Display(Name = "Водоотведение")]
        public string водоотведение { get; set; }
        [Display(Name = "Система водостоков")]
        public string система_водостоков { get; set; }
        [Display(Name = "Газоснабжение")]
        public string газоснабжение { get; set; }
        [Display(Name = "Горячее водоснабжение")]
        public string горячее_водоснаб { get; set; }
        [Display(Name = "Холодное водоснабжение")]
        public string холодное_водоснаб { get; set; }
        [Display(Name = "Теплоснабжение")]
        public string теплоснабжение { get; set; }
        [Display(Name = "Электроснабжение")]
        public string электроснабжение { get; set; }
        [Display(Name = "Материал отделки фасада")]
        public string мат_отделки_фасада { get; set; }
        [Display(Name = "Форма крыши")]
        public string форма_крыши { get; set; }
        [Display(Name = "Утепляющие слои чердачных перекрытий")]
        public string утепляющие_слои_чердачных_перекрытий { get; set; }
        [Display(Name = "Материал окон")]
        public string мат_окон { get; set; }
        [Display(Name = "Материал сети отопления")]
        public string мат_сети_отопления { get; set; }
        [Display(Name = "Материал теплоизоляции сети")]
        public string мат_теплоизоляции_сети { get; set; }
        public virtual ICollection<Мероприятия> Мероприятия { get; set; }
        public virtual Состояние_дома Состояние_дома { get; set; }
        public virtual Способ_управления_МКД Способ_управления_МКД { get; set; }
        public virtual Управляющие_компании Управляющие_компании { get; set; }
        public virtual ICollection<Обращения> Обращения { get; set; }
    }
}
