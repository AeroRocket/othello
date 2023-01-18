using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Reverse
{
    public partial class Form1 : Form
    {
        Points[,] board = new Points[8, 8];

        List<int> X_range = new List<int>();
        List<int> Y_range = new List<int>();

        bool color_stage = false;

        public int move_count = 0;

        public List<Tuple<int, int>> data_dump = new List<Tuple<int, int>>();
        public List<Tuple<int, int>> content = new List<Tuple<int, int>>();
        int color_for_change = 0;

        // none = 0, black = 1, white = 2
        public Form1()
        {
            InitializeComponent();
            int tmp_x;
            int tmp_y;



            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    int x = (pictureBox1.Width / 8 + 2) * j;
                    int y = (pictureBox1.Height / 8 + 2) * i;


                    board[i, j] = new Points(x, y, 0);
                }
            }
            // initial game position

            board[3, 3].color = 1;
            board[4, 4].color = 1;
            board[3, 4].color = 2;
            board[4, 3].color = 2;


            for (int x = 0; x < 8; x++)
            {
                tmp_x = pictureBox1.Width / 8 * x;
                X_range.Add(tmp_x);
            }
            for (int y = 0; y < 8; y++)
            {
                tmp_y = pictureBox1.Width / 8 * y;
                Y_range.Add(tmp_y);
            }
            // wowrk here


        }

        private void Draw(Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].color == 1) {
                        g.FillEllipse(Brushes.Black, board[i, j].pos_x + 17, board[i, j].pos_y + 2, 40, 40);
                    }
                    if (board[i, j].color == 2)
                    {
                        g.FillEllipse(Brushes.White, board[i, j].pos_x + 17, board[i, j].pos_y + 2, 40, 40);
                    }

                }
            }

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            Graphics g = e.Graphics;

            for (int i = 0; i < 41; i++)
            {
                g.DrawLine(new Pen(Color.Black, 2), (i * pictureBox1.Width / 8), 0, (i * pictureBox1.Width / 8), pictureBox1.Height);
            }

            for (int j = 0; j < 41; j++)
            {
                g.DrawLine(new Pen(Color.Black, 2), 0, (j * pictureBox1.Height / 8), pictureBox1.Width, (j * pictureBox1.Height / 8));
            }

            Draw(g);


        }
        private bool Position_check(int x, int y, int color, int move_count)
        {
            int color_wanted = 0;

            if (color == 1)
            {
                color_wanted = 2;
            }
            else if (color == 2)
            {
                color_wanted = 1;
            }

            if (move_count == 0)
            {
                if (y != 7) {
                    if (board[y + 1, x].color == color_wanted)
                    {
                        return true;
                    }
                }
                if (x != 7) {
                    if (board[y, x + 1].color == color_wanted)
                    {
                        return true;
                    }
                }

                if (y != 0)
                {
                    if (board[y - 1, x].color == color_wanted)
                    {
                        return true;
                    }

                }

                if (x != 0)
                {
                    if (board[y, x - 1].color == color_wanted)
                    {
                        return true;
                    }
                }
                return false;


            }
            else
            {
                if (y != 0)
                {
                    if (board[y - 1, x].color == color_wanted)
                    {
                        return true;
                    }

                    if (x != 7)
                    {
                        if (board[y - 1, x + 1].color == color_wanted)
                        {
                            return true;
                        }
                    }

                    if (x != 0)
                    {
                        if (board[y - 1, x - 1].color == color_wanted)
                        {
                            return true;
                        }
                    }


                }

                if (y != 7)
                {
                    if (board[y + 1, x].color == color_wanted)
                    {
                        return true;
                    }
                    if (x != 0)
                    {
                        if (board[y + 1, x - 1].color == color_wanted)
                        {
                            return true;
                        }
                    }
                    if (x != 7)
                    {
                        if (board[y + 1, x + 1].color == color_wanted)
                        {
                            return true;
                        }
                    }
                }


                if (x != 7)
                {
                    if (board[y, x + 1].color == color_wanted)
                    {
                        return true;
                    }
                }
               
               if (x != 0)
                {
                    if (board[y, x - 1].color == color_wanted)
                    {
                        return true;
                    }
                }
                
                
            }
            return false;
        }

        public bool end_of_game()
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (board[y, x].color == 0)
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        
        public void attack_dir(int y, int x, int color)
        {
            int color_wanted = 0;

            if (color == 1)
            {
                color_wanted = 2;
            }
            else if (color == 2)
            {
                color_wanted = 1;
            }


            //////////////

            data_dump.Clear();
            

            if (y != 7)
            {
                if (board[y + 1, x].color == color_wanted)
                {
                    var tmp = Tuple.Create(1,0);
                    data_dump.Add(tmp);

                }
            }
            if (x != 7)
            {
                if (board[y, x + 1].color == color_wanted)
                {
                    var tmp = Tuple.Create(0, 1);
                    data_dump.Add(tmp);
                }
            }

            if (y != 0)
            {
                if (board[y - 1, x].color == color_wanted)
                {
                    var tmp = Tuple.Create(-1, 0);
                    data_dump.Add(tmp);
                }

            }

            if (x != 0)
            {
                if (board[y, x - 1].color == color_wanted)
                {
                    var tmp = Tuple.Create(0, -1);
                    data_dump.Add(tmp);
                }
            }



            if (y != 0)
            {
                if (board[y - 1, x].color == color_wanted)
                {
                    var tmp = Tuple.Create(-1, 0);
                    data_dump.Add(tmp);
                }

                if (x != 7)
                {
                    if (board[y - 1, x + 1].color == color_wanted)
                    {
                        var tmp = Tuple.Create(-1, 1);
                        data_dump.Add(tmp);
                    }
                }

                else if (x != 0)
                {
                    if (board[y - 1, x - 1].color == color_wanted)
                    {
                        var tmp = Tuple.Create(-1, -1);
                        data_dump.Add(tmp);
                    }
                }


            }

            if (y != 7)
            {
                if (board[y + 1, x].color == color_wanted)
                {
                    var tmp = Tuple.Create(1, 0);
                    data_dump.Add(tmp);
                }
                if (x != 0)
                {
                    if (board[y + 1, x - 1].color == color_wanted)
                    {
                        var tmp = Tuple.Create(1, -1);
                        data_dump.Add(tmp);
                    }
                }
                else if (x != 7)
                {
                    if (board[y + 1, x + 1].color == color_wanted)
                    {
                        var tmp = Tuple.Create(1, 1);
                        data_dump.Add(tmp);
                    }
                }
            }


            if (x != 7)
            {
                if (board[y, x + 1].color == color_wanted)
                {
                    var tmp = Tuple.Create(0, 1);
                    data_dump.Add(tmp);
                }
            }
            if (x != 0)
            {
                if (board[y, x - 1].color == color_wanted)
                {
                    var tmp = Tuple.Create(0, -1);
                    data_dump.Add(tmp);
                }
            }
        }


        public  void attack_exec(int y, int x, int color)
        {
            int color_wanted = 0;

            if (color == 1)
            {
                color_wanted = 2;
            }
            else if (color == 2)
            {
                color_wanted = 1;
            }

            color_for_change = color_wanted;

            content.Clear();
            attack_dir(y, x, color);
            

            for (int t=0; t<data_dump.Count; t++)
            {

                int current_y = data_dump[t].Item1;
                int current_x = data_dump[t].Item2;
                Console.WriteLine(data_dump[t].Item1);

                for (int i = 0; i < 7; i++)
                {
                    current_y += data_dump[t].Item1;
                    current_x += data_dump[t].Item2;


                    if (current_x < 0 && current_x > 7 && current_y < 0 && current_y > 7)
                    {
                        if (board[current_y, current_x].color == color_wanted)
                        {
                            var tmp = Tuple.Create(current_y, current_y);
                            content.Add(tmp);

                        }
                    }
                    else
                    {
                        break;
                    }

                }
            }
            Console.WriteLine(content);

        }

        public void Draw_attack() { 

            for (int i=0; i<content.Count; i++)
            {
                board[content[i].Item1, content[i].Item2].color = color_for_change;
            }
        }



        public void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {  
            int click_x = e.X;
            int click_y = e.Y;

            int current_color = 0;


            if (color_stage == false)
            {
                current_color = 1;
            }
            else
            {
                current_color = 2;
            }


            int tmp_x = click_x * 8 / pictureBox1.Width;
            int tmp_y = click_y * 8 / pictureBox1.Height;

            if (end_of_game() == false)
            {


                if (board[tmp_y, tmp_x].color == 0)
                {
                    if (Position_check(tmp_x, tmp_y, current_color, move_count) == true)
                    {
                        attack_exec(tmp_x, tmp_y, current_color);
                        Draw_attack();
                        if (content.Count != 0)
                        {
                            textBox1.Text = "White";
                        }
                        move_count++;
                        
                        if (color_stage == false)
                        {
                            board[tmp_y, tmp_x].color = 1;
                            color_stage = true;
                            textBox1.Text = "White";
                        }
                        else
                        {
                            board[tmp_y, tmp_x].color = 2;
                            color_stage = false;
                            textBox1.Text = "Black";
                        }
                    }

                }
            }

            pictureBox1.Invalidate();

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}   