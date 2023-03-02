using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Models
{
    internal class EmailSettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string FromAdress { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }
}
