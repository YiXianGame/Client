using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make.MODEL
{
    public class Pos
    {
        private string name = "🌾";
        private int x;
        private int y;
        private object item;
        private string informations;

        public string Informations { get => informations; set => informations = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public string Name { get => name; set => name = value; }
        public object Item { get => item; set => item = value; }

    }
}
