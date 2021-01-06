using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Data
{
    public class BaseEntity
    {
        public Int64 Id { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; }
    }
}
