using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Student 
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(10000,20000)]
        public int Indeks { get; set; }

        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }

        public List<Spoj> StudentPredmet { get; set; }

        
    }
}