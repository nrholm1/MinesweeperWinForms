using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperPart2
{
    class XYButton : System.Windows.Forms.Button
    {
        /*------------GameLogic parameters and methods-----------*/
        bool isMine = false;
        bool isFlagged = false;
        bool isClicked = false;

        public bool IsMine
        {
            get { return isMine; }
            set { isMine = value; }
        }
        public bool IsFlagged
        {
            get { return isFlagged; }
            set { isFlagged = value; }
        }
        public bool IsClicked
        {
            get { return isClicked; }
            set { isClicked = value; }
        }
        /*------------Positional parameters and methods-----------*/
        int posX;
        int posY;
        public int PosX {
            get { return posX; }
            set { posX = value; }
        }
        public int PosY {
            get { return posY; }
            set { posY = value; }
        }

        /*------------Constructor-----------*/
        public XYButton(int PosX, int PosY)
        {
            posX = PosX;
            posY = PosY;
        }
    }
}
