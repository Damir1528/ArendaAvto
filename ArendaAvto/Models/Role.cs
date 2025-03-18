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
    [Table("role")]
    public class Role: BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("role")]
        public string Roles { get; set; }
    }
}
