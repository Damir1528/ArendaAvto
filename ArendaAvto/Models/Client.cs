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
    [Table("client")]
    public class Client: BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("lastname")]
        public string Lastname { get; set; }

        [Column("firstname")]
        public string Firstname { get; set; }

        [Column("secondname")]
        public string Secondname { get; set; }

        [Column("number_VU")]
        public string Number_VU { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("bonus")]
        public int Bonus { get; set; }
    }
}
