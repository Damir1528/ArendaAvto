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
    [Table("arenda")]
    public class Arenda: BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("id_car")]
        public int CarId { get; set; }

        [Column("id_client")]
        public int ClientId { get; set; }
        public Cars Car { get; set; } // Объект Cars
        public Client Client { get; set; } // Объект Client

        [Column("data_vidachi")]
        public string DataVidachi { get; set; }

        [Column("data_vozvrat")]
        public string DataVozvrat { get; set; }

        [Column("data_fuct_vozvrat")]
        public string DataFuctVozvrat { get; set; }

        [Column("probeg_do")]
        public int ProbegDo { get; set; }

        [Column("probeg_posle")]
        public int ProbegPosle { get; set; }

        [Column("cost")]
        public int Cost { get; set; }

        [Column("ocenka_clienta")]
        public int Ocenka { get; set; }

        [Column("comment")]
        public string Comment { get; set; }
    }
}
