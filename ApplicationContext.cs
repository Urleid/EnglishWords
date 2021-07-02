using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Urleid.EnglishWords
{
    class ApplicationContext : DbContext
    {
        public DbSet<Word> Words { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
