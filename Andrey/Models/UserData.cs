using System.ComponentModel.DataAnnotations;

namespace Andrey.Models
{
    public class UserData
    {
        [Key]
        public int UId { get; set; }
        public User User { get; set; }

        public UserData()
        {
            this.User = new Models.User();
        }
    }
}
