using ABC_Bank.DBContext;
using ABC_Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABC_Bank.Repositories
{
    public class ContactInfoRepository : IDisposable
    {
        public ContactInfoContext Db
        {
            get;
            private set;
        }

        public ContactInfoRepository()
        {
            this.Db = new ContactInfoContext();
        }

        public IEnumerable<ContactInfo> GetContactsByName(string searchPattern)
        {
            List<ContactInfo> foundContacts = new List<ContactInfo>();
            foreach (var p in Db.DbSet)
            {
                if ($"{p.Firstname} {p.Secondname}".ToLower().Contains(searchPattern.ToLower()))
                    foundContacts.Add(p);
            }

            return foundContacts;
        }

        public IEnumerable<ContactInfo> GetContactsByAddress(string searchPattern)
        {
            List<ContactInfo> foundContacts = new List<ContactInfo>();
            foreach (var p in Db.DbSet)
            {
                if ($"{p.Country} {p.City} {p.Streetname} {p.Housenumber}".ToLower().Contains(searchPattern.ToLower()))
                    foundContacts.Add(p);
            }

            return foundContacts;
        }

        public IEnumerable<ContactInfo> GetContactsByAge(string searchPattern)
        {
            var ages = searchPattern.Split(',');
            if(!double.TryParse(ages[0].Trim(), out var min_age))
                throw new ArgumentException($"The searchpattern to retrieve contacts by age was invalid. Minimum age of {ages[0]} is not a number.");

            if (!double.TryParse(ages[1].Trim(), out var max_age))
                throw new ArgumentException($"The searchpattern to retrieve contacts by age was invalid. Maximum age of {ages[1]} is not a number.");

            if (max_age < min_age)
                throw new ArgumentException($"The searchpattern to retrieve contacts by age was invalid. Maximum age was smaller than minimum age.");

            if (min_age < 0)
                throw new ArgumentException($"The searchpattern to retrieve contacts by age was invalid. Minimum age was smaller than 0.");

            if (max_age < 1)
                throw new ArgumentException($"The searchpattern to retrieve contacts by age was invalid. Maximum age was smaller than 1.");

            List<ContactInfo> foundContacts = new List<ContactInfo>();
            foreach (var p in Db.DbSet)
            {
                if (GetAge(p.DateOfBirth) >= min_age && GetAge(p.DateOfBirth) <= max_age)
                    foundContacts.Add(p);
            }

            return foundContacts;
        }

        public bool AddContact(ContactInfo product)
        {
            Db.DbSet.Add(product);
            return SaveChanges();
        }

        public int GetAmount()
        {
            return Db.DbSet.Count();
        }

        public void Dispose()
        {
            if (Db != null)
            {
                Db.Dispose();
                Db = null;
            }

            GC.SuppressFinalize(this);
        }

        private bool SaveChanges()
        {
            try
            {
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        private double GetAge(string date)
        {
            var today = DateTime.Today;
            try
            {
                var birthdate = DateTime.Parse(date);
                var age = today.Year - birthdate.Year;
                if (DateTime.Now.DayOfYear < birthdate.DayOfYear)
                    age -= 1;

                return age;
            }
            catch
            {
                return -1;
            }
        }
    }
}