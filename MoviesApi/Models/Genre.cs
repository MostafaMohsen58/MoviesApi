using System.ComponentModel.DataAnnotations.Schema;



namespace MoviesApi.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
