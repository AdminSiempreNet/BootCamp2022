using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Core
{
    public class TypedResult<T> : MsgResult
    {
        public T Model { get; set; }
    }
}
