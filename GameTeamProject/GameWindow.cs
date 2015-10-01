using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTeamProject
{
    public partial class GameWindow : Form
    {
        public static Game game;
        private static readonly object locker = new object();
        public GameWindow()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.canvas.CreateGraphics();
            game = new Game(g);

            game.Start();
        }

        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.Stop();
        }

        private void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            game.HandleKeyUp(e);
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            game.HandleKeyDown(e);
        }
    }
}
