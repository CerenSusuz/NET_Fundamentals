using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetFundamentals.DLL
{
    internal static class AdoNetDbContext
    {
        public static string ConnectionString { get; set; } =
            "Server=., 1433;Database=ADONETDb;User Id=SqlUser;Password=Pass123;TrustServerCertificate=True;";
    }
}
