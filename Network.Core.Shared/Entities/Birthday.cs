using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Core.Shared.Entities
{
    public class Birthday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Note { get; set; }

        [DisplayName("Загруженный файл")]
        public string Photo { get; set; }
        [NotMapped]
        public  IFormFile ImageFile { get; set; }
    }
}
