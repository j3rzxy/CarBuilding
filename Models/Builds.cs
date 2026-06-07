using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBuilding.Models
{
    internal class Builds
    {
        public int BuildID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public bool IsPublic {  get; set; }
    }
}
