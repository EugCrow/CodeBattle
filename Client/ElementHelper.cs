using SnakeBattle.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class ElementHelper
    {
        public static bool IsWall(BoardElement element)
        {
            return element == BoardElement.Wall;
        }
    }
}
