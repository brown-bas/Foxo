using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxo
{
    public class SaveFileException : Exception
    {
        public SaveFileException() : base("Hiba történt a slot betöltésekor, lehet, hogy a mentés meghibásodott!") { }
    }
}