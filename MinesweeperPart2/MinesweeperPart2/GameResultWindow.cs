using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperPart2
{
    public partial class GameResultWindow : Form
    {
        public GameResultWindow()
        {
            InitializeComponent();
        }

        private void GameResultWindow_Load(object sender, EventArgs e)
        {

        }

        public string GameResultText
        {
            get
            {
                return this.GameResultMessage.Text;
            }
            set
            {
                this.GameResultMessage.Text = value;
            }
        }
    }
}
