using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class Vector2Extension
    {
        public static Vector2 Round(this Vector2 vector, ushort decimals) {

            var x = float.Round(vector.X, decimals);
            var y = float.Round(vector.Y, decimals);
            return new Vector2(x, y);

        }
    }
}
