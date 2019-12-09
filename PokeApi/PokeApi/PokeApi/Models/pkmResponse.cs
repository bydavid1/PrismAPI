using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApi.Models
{
    public class pkmResponse
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Pokemon> results { get; set; }
    }
}
