using System;
using System.ComponentModel.DataAnnotations;

namespace EFcore_testing
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static implicit operator User(Range v)
        {
            throw new NotImplementedException();
        }
    }
}
