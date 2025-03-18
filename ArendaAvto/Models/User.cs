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
    [Table("user")]
    public class User: BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("roleid")]
        public int RoleId { get; set; }

        public Role role { get; set; }

        [Column("password")]
        public string Password { get; set; }
    }
}
