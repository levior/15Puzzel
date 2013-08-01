using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App6
{
    public class PositionXY
    {
        private int x;
        private int y;

        public PositionXY()
        {
            x = 0;
            y = 0;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetX(int x1)
        {
            x = x1;
        }

        public void SetY(int y1)
        {
            y = y1;
        }

    }
}
