using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestFirebird.Model
{
    [Table("Stdt")]
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]

        public string Name { get; set; }

        [ForeignKey("University")]
        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}
