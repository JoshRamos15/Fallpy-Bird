using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird_Windows_Form
{
    public partial class Form1 : Form
    {
        // coded for MOO ICT Flappy Bird Tutorial

        // Variables start here
        int pipeSpeed = 8; // default pipe speed defined with an integer
        int gravity = 15; // default gravity speed defined with an integer
        int score = 0; // default score integer set to 0
        int highScore = 0; // high score tracking
        Random rand = new Random(); // for randomizing pipe heights

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;  // Set KeyPreview property to true to capture key events
            this.KeyDown += new KeyEventHandler(gamekeyisdown);
            this.KeyUp += new KeyEventHandler(gamekeyisup);
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -15;
            }
            else if (e.KeyCode == Keys.P)
            {
                TogglePauseGame(); // toggle pause
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 15;
            }
        }

        private void TogglePauseGame()
        {
            if (gameTimer.Enabled)
            {
                gameTimer.Stop();
                scoreText.Text = "Paused";
            }
            else
            {
                gameTimer.Start();
                scoreText.Text = "Score: " + score;
            }
        }

        private void endGame()
        {
            gameTimer.Stop(); // stop the main timer
            SystemSounds.Exclamation.Play(); // play sound on game over
            if (score > highScore)
            {
                highScore = score;
                scoreText.Text += " New High Score!";
            }
            scoreText.Text += " Game over!!!"; // show the game over text on the score text
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score: " + score;

            if (pipeBottom.Left < -150)
            {
                pipeBottom.Left = 800;
                pipeBottom.Top = rand.Next(200, 400); // randomize bottom pipe height
                score++;
                SystemSounds.Beep.Play(); // play a beep sound when the score increases
            }
            if (pipeTop.Left < -180)
            {
                pipeTop.Left = 950;
                pipeTop.Top = rand.Next(-200, 100); // randomize top pipe height
                score++;
                SystemSounds.Beep.Play(); // play a beep sound when the score increases
            }

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < -25)
            {
                endGame();
            }

            if (score > 5)
            {
                pipeSpeed = 15;
            }

            if (score > 10)
            {
                flappyBird.BackColor = Color.Red; // change color of Flappy Bird
            }
        }
    }
}
   