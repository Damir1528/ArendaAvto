using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase; 
using System.Net;
using Supabase.Postgrest.Attributes;

namespace ArendaAvto.Models
{
    [Table("cars")]
    public class Cars: BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("marka")]
        public string Marka { get; set; }

        [Column("model")]
        public string Model { get; set; }

        [Column("year_create")]
        public string Year_create { get; set; }

        [Column("type_car")]
        public string Type_car { get; set; }

        [Column("class")]
        public string Class { get; set; }

        [Column("cost_24_hours")]
        public int Cost { get; set; }

        [Column("gos_number")]
        public string Gos_number { get; set; }

        [Column("probeg")]
        public int Probeg { get; set; }

        [Column("sostoyanie")]
        public string Sostoyanie { get; set; }

        [Column("photo")]
        public string Photo { get; set; }

        [Column("dostup")]
        public string Dostup { get; set; }

        [Column("kolvo_toplivo")]
        public int Toplivo { get; set; }
    }
}
