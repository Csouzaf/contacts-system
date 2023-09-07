using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models.auth.Model;

namespace api.Models
{
    [Table("userRegisteredModel")]
    public class UserRegisteredModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //NOTE - Relantionship 1 to 1 with UsersAuth
        [ForeignKey("UsersAuth")]
        public int usersAuthenticatedId  {get; set; }
        
        public UsersAuth? usersAuth { get; set; }
            
        [Required]   
        public ICollection<ContactsModel> colletctionContactsModels { get; } = new List<ContactsModel>();


    }
}