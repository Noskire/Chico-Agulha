using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class PlayerEstadoException : Exception
    {
        public PlayerEstadoException(string msg):base(msg)
        {
        }
    }
}
