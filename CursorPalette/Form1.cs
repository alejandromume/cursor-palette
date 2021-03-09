using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Utilities;
using System.Threading;

namespace CursorPalette
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);
        public Form1()
        {
            InitializeComponent();

        }

        void ghk_KeyDown(object sender, KeyEventArgs e)
        {
            
            this.Cursor = new Cursor(Cursor.Current.Handle);
            int posX = Cursor.Position.X;
            int posY = Cursor.Position.Y;
            panel1.BackColor = GetColorAt(posX, posY);
            textBox2.Text = $"{GetColorAt(posX, posY).R.ToString()}, {GetColorAt(posX, posY).G.ToString()}, {GetColorAt(posX, posY).B.ToString()}";
            textBox3.Text = RGBtoHex($"{GetColorAt(posX, posY).R.ToString()}", $"{GetColorAt(posX, posY).G.ToString()}", $"{GetColorAt(posX, posY).B.ToString()}");
            
            if(guna2CustomCheckBox1.Checked == true)
            {
                Console.Beep(1000, 250);
            }

            if(guna2CustomCheckBox3.Checked == true)
            {
                Clipboard.SetText(RGBtoHex($"{GetColorAt(posX, posY).R.ToString()}", $"{GetColorAt(posX, posY).G.ToString()}", $"{GetColorAt(posX, posY).B.ToString()}"));
            }
        }


        public static Color GetColorAt(int x, int y)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        public string RGBtoHex(string r, string g, string b)
        {
            Color myColor = Color.FromArgb(Int32.Parse(r), Int32.Parse(g), Int32.Parse(b));
            return myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(guna2CustomCheckBox2.Checked == true)
            {
                Application.Exit();
            }
            else
            {
                Hide();
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        }

        private void copyRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                if (textBox3.Text != "")
                    Clipboard.SetText(textBox2.Text);
        }

        private void copyHexToolStripMenuItem_Click(object sender, EventArgs e)
            {
               
                    if (textBox3.Text != "")
                        Clipboard.SetText(textBox3.Text);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string str = null;
                str = dlg.Color.Name;
                textBox2.Text = $"{dlg.Color.R}, {dlg.Color.G}, {dlg.Color.B}";
                textBox3.Text = RGBtoHex(dlg.Color.R.ToString(), dlg.Color.G.ToString(), dlg.Color.B.ToString());
                panel1.BackColor = Color.FromArgb(dlg.Color.R, dlg.Color.G, dlg.Color.B);

            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.Cursor = Cursors.Hand;

        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
        }

        async private void guna2TileButton1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            //guna2TileButton1.Text = e.KeyCode.ToString();
            
        }

        private void guna2TileButton1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                Clipboard.SetText(textBox2.Text);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                Clipboard.SetText(textBox3.Text);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2Button4.Text = "< Press a key >";
        }

        private void guna2Button4_KeyPress(object sender, KeyPressEventArgs e)
        {
            guna2Button4.Text = e.KeyChar.ToString().ToUpper();
        }

        private void guna2Button4_KeyDown(object sender, KeyEventArgs e)
        {
            globalKeyboardHook ghk = new globalKeyboardHook();
            ghk.HookedKeys.Add(e.KeyCode);
            ghk.KeyDown += new KeyEventHandler(ghk_KeyDown);
        }
    }
}
