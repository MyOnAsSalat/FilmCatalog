using System.ComponentModel.DataAnnotations;

namespace JobTest.Models
{
    public class Film
    {
        [Key] public int FilmId { get; set; }

        [Required] [MaxLength(256)] public string Title { get; set; }

        [MaxLength(10000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required] public int Year { get; set; }

        [MaxLength(256)] [Required] public string Producer { get; set; }

        public byte[] Poster { get; set; }
        public string User { get; set; }
    }
}