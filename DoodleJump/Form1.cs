using DoodleJump.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoodleJump
{
    public partial class Form1 : Form
    {
        Player player;
        Timer timer1;
        private bool gameOver;
        private PictureBox playAgainImage;
        public Form1()
        {
            InitializeComponent();
            Init();
            timer1 = new Timer();
            timer1.Interval = 15;
            timer1.Tick += new EventHandler(Update);
            timer1.Start();
            this.KeyDown += new KeyEventHandler(OnKeyboardPressed);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.BackgroundImage = Properties.Resources.bak;
            this.Height = 880;
            this.Width = 409;
            this.Paint += new PaintEventHandler(OnRepaint);
        }

        public void Init()
        {
            PlatformController.platforms = new System.Collections.Generic.List<Platform>();
            PlatformController.AddPlatform(new System.Drawing.PointF(100, 400));
            PlatformController.startPlatformPosY = 400;
            PlatformController.score = 0;
            PlatformController.GenerateStartSequence();
            PlatformController.bullets.Clear();
            PlatformController.bonuses.Clear();
            PlatformController.enemies.Clear();
            player = new Player();
        }

        private void OnKeyboardUp(object sender, KeyEventArgs e)
        {
            player.physics.dx = 0;
            player.sprite = Properties.Resources.doodler;
            switch (e.KeyCode.ToString())
            {
                case "Space":
                    PlatformController.CreateBullet(new PointF(player.physics.transform.position.X + player.physics.transform.size.Width / 2, player.physics.transform.position.Y));
                    break;
            }
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    player.physics.dx = 6;
                    break;
                case "Left":
                    player.physics.dx = -6;
                    break;
                case "Space":
                    player.sprite = Properties.Resources.doodler_gun;
                    break;
            }
        }

        private void PlayAgainImage_Click(object sender, EventArgs e)
        {
            Init();
            gameOver = false;

            if (playAgainImage != null)
            {
                playAgainImage.Hide();
            }
        }
        private void Update(object sender, EventArgs e)
        {
            if ((player.physics.transform.position.Y >= PlatformController.platforms[0].transform.position.Y + 600) ||
                player.physics.StandartCollidePlayerWithObjects(true, false))
            {
                gameOver = true;

                if (playAgainImage == null)
                {
                    playAgainImage = new PictureBox();
                    playAgainImage.Image = Properties.Resources.play_again;
                    playAgainImage.SizeMode = PictureBoxSizeMode.StretchImage; // Меняем режим отображения на растягивание
                    playAgainImage.Size = new Size(168, 78); // Устанавливаем новый размер изображения
                    playAgainImage.Click += PlayAgainImage_Click;

                    int x = (ClientSize.Width - playAgainImage.Width) / 2;
                    int y = (ClientSize.Height - playAgainImage.Height) / 2;
                    playAgainImage.Location = new Point(x, y);

                    Controls.Add(playAgainImage);
                }

                return;
            }

            if (gameOver)
                return;




            this.Text = "Doodle Jump: Score - " + PlatformController.score;
            player.physics.StandartCollidePlayerWithObjects(false, true);

            if (PlatformController.bullets.Count > 0)
            {
                for (int i = 0; i < PlatformController.bullets.Count; i++)
                {
                    if (Math.Abs(PlatformController.bullets[i].physics.transform.position.Y - player.physics.transform.position.Y) > 500)
                    {
                        PlatformController.RemoveBullet(i);
                        continue;
                    }
                    PlatformController.bullets[i].MoveUp();
                }
            }
            if (PlatformController.enemies.Count > 0)
            {
                for (int i = 0; i < PlatformController.enemies.Count; i++)
                {
                    if (PlatformController.enemies[i].physics.StandartCollide())
                    {
                        PlatformController.RemoveEnemy(i);
                        break;
                    }
                }
            }

            player.physics.ApplyPhysics();
            FollowPlayer();

            Invalidate();
        }

        public void FollowPlayer()
        {
            int offset = 400 - (int)player.physics.transform.position.Y;
            player.physics.transform.position.Y += offset;
            for (int i = 0; i < PlatformController.platforms.Count; i++)
            {
                var platform = PlatformController.platforms[i];
                platform.transform.position.Y += offset;
            }
            for (int i = 0; i < PlatformController.bullets.Count; i++)
            {
                var bullet = PlatformController.bullets[i];
                bullet.physics.transform.position.Y += offset;
            }
            for (int i = 0; i < PlatformController.enemies.Count; i++)
            {
                var enemy = PlatformController.enemies[i];
                enemy.physics.transform.position.Y += offset;
            }
            for (int i = 0; i < PlatformController.bonuses.Count; i++)
            {
                var bonus = PlatformController.bonuses[i];
                bonus.physics.transform.position.Y += offset;
            }
        }

        private void OnRepaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (PlatformController.platforms.Count > 0)
            {
                for (int i = 0; i < PlatformController.platforms.Count; i++)
                    PlatformController.platforms[i].DrawSprite(g);
            }
            if (PlatformController.bullets.Count > 0)
            {
                for (int i = 0; i < PlatformController.bullets.Count; i++)
                    PlatformController.bullets[i].DrawSprite(g);
            }
            if (PlatformController.enemies.Count > 0)
            {
                for (int i = 0; i < PlatformController.enemies.Count; i++)
                    PlatformController.enemies[i].DrawSprite(g);
            }
            if (PlatformController.bonuses.Count > 0)
            {
                for (int i = 0; i < PlatformController.bonuses.Count; i++)
                    PlatformController.bonuses[i].DrawSprite(g);
            }
            player.DrawSprite(g);
        }
    }
}
