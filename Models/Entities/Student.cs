using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string Name { get; set; } = null!;
        public HouseType? HouseType { get; set; }
        public PetType? PetType { get; set; }

        [JsonIgnore]
        public Room? Room { get; set; }
    }
}
