using System;
using System.Collections.Generic;

namespace Cubo.Core.DTOs
{
    public class BucketDTO
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<string> Items { get; set; }
    }
}