using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC_Bank.Models
{
    [Table(DatabaseStrings.ContactsTable)]
    public class ContactInfo : IEquatable<ContactInfo>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Secondname { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Streetname { get; set; }

        [Required]
        public string Housenumber { get; set; }

        [Required]
        public string Phonenumber { get; set; }

        public byte[] ProfilePicture { get; set; }

        public bool Equals(ContactInfo other)
        {
            return this.ID == other.ID;
        }
    }
}