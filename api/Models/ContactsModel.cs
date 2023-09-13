using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Models.auth.Model;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("contacts")]
    public class ContactsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;


        [ForeignKey("UsersAuth")]
        [Required]
        public int userRegisteredId { get; set; }

        [JsonIgnore] //NOTE - Because the UsersAuth will be salved as field required 
        public UsersAuth? UsersAuth { get; set; }


    }
}