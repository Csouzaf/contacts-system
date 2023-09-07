using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Models.auth.Model;

namespace api.auth.Model
{
    [Table("AuthenthicateUserEmail")]
    public class AuthUserEmail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        
        public string? Email { get; set; } 

        [ForeignKey("UsersAuth")]        
        public int userAuthId { get; set; }
        
        [Required]
        public UsersAuth? UsersAuth { get; set; } 
       
    }
}