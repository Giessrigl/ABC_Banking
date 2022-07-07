using ABC_Bank.DBContext.Interfaces;
using ABC_Bank.Models;

namespace ABC_Bank.DBContext
{
    public class ContactInfoContext : DatabaseContext<ContactInfoContext, ContactInfo>
    {
        public ContactInfoContext() : base()
        {
        }
    }
}