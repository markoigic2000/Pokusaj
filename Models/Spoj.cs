using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Spoj
    {
        [Key]
        public int ID { get; set; }
        [Range(5,10)]
        public int Ocena { get; set; }

        [JsonIgnore]
        public Student Student { get; set; }

        public Predmet Predmet { get; set; }

        public IspitniRok IspitniRok { get; set; }
    }
}