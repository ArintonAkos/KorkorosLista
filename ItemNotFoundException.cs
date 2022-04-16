using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorkorosLista
{
    internal class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() : base("Item is not in the list!")
        {
        }
    }
}
