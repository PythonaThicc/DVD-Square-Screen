using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVD_Screen
{
    public class Program : Form
    {
        private const int BOX_SIZE = 50;
        private const int BOX_SPEED = 5;
        private readonly Random random = new Random();

        private readonly Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
        private readonly Timer timer = new Timer();
        private Point boxPosition;
        private Point boxDirection;
        private Color boxColor;

        private readonly Color[] colors = new Color[]
        {
            Color.Green,
            Color.Red,
            Color.Blue,
            Color.Orange,
            Color.Brown,
            Color.Black,
            Color.White,
            Color.Purple
        };

        public static void Main()
        {
            Application.Run(new Program());
        }

        public Program()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Gray;
            this.DoubleBuffered = true;

            this.boxPosition = new Point(
                this.random.Next(0, this.screenBounds.Width - BOX_SIZE),
                this.random.Next(0, this.screenBounds.Height - BOX_SIZE));

            int xDir = this.random.Next(-1, 2);
            if (xDir == 0)
            {
                xDir = 1;
            }
            int yDir = this.random.Next(-1, 2);
            if (yDir == 0)
            {
                yDir = 1;
            }
            this.boxDirection = new Point(xDir * BOX_SPEED, yDir * BOX_SPEED);

            this.boxColor = GetRandomColor();

            this.timer.Interval = 20;
            this.timer.Tick += new EventHandler(this.OnTimerTick);
            this.timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this.boxPosition.X += this.boxDirection.X;
            this.boxPosition.Y += this.boxDirection.Y;

            if (this.boxPosition.X < 0 || this.boxPosition.X + BOX_SIZE > this.screenBounds.Width)
            {
                this.boxDirection.X *= -1;
                this.boxColor = GetRandomColor();
            }

            if (this.boxPosition.Y < 0 || this.boxPosition.Y + BOX_SIZE > this.screenBounds.Height)
            {
                this.boxDirection.Y *= -1;
                this.boxColor = GetRandomColor();
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (SolidBrush brush = new SolidBrush(this.boxColor))
            {
                e.Graphics.FillRectangle(brush, new Rectangle(this.boxPosition, new Size(BOX_SIZE, BOX_SIZE)));
            }
        }

        private Color GetRandomColor()
        {
            return this.colors[this.random.Next(this.colors.Length)];
        }
    }
}
