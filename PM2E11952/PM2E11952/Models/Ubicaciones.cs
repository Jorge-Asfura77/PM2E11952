using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PM2E11952.Models
{
    public class Ubicaciones
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        [MaxLength(70)]
        public Double latitud { get; set; }

        [MaxLength(70)]
        public Double longitud { get; set; }

        [MaxLength(250)]
        public string descripcion { get; set; }

        public byte[] imagen { get; set; }
    }
}
