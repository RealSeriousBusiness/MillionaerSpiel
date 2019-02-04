
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Millionär
{
    public partial class Main : Form
    {
        Intro intro;
        String[] lines = {""};
        public Main(Intro intro)
        {
            InitializeComponent();
            try
            {
                lines = File.ReadAllText("content.txt").Split('\n');
            }
            catch (Exception) 
            { 
                MessageBox.Show("Konnte Fragendatei nicht laden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Program.bigscreen)
                this.Size = new Size(1024, 768);
            this.intro = intro;
            this.intro.Hide();
        }

        QuestionLoader q;
        
        bool won = true, unanwsered = true;
        char chooseAnswer = 'E';
        byte currentQuestion = 1;
        int[] gewinne = {0, 50, 100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 500000, 1000000 };
        bool _5050 = true, telefon = true, publikum = true;
        
        private void Main_Load(object sender, EventArgs e)
        {
            NextQuestion();
        }

        private void markYellow(Label mark)
        {
            mark.BackColor = Color.FromArgb(200, 200, 0);
            mark.ForeColor = Color.Black;
        }

        private void markGreen(Label mark)
        {
            mark.BackColor = Color.FromArgb(0, 200, 0);
            mark.ForeColor = Color.Black;
        }

        private void markRed( Label red)
        {
            red.BackColor = Color.FromArgb(200, 0, 0);
            red.ForeColor = Color.Black;
        }

        private void labelAn1_Click(object sender, EventArgs e)
        {
            if (unanwsered)
            {
                setAnswerBox(true, true, pictureBoxTopLeft, Millionär.Properties.Resources.topleftYellow);
                markYellow(labelAn1);
                chooseAnswer = 'A';
            }
        }

        private void labelAn2_Click(object sender, EventArgs e)
        {
            if (unanwsered)
            {
                setAnswerBox(true, true, pictureBoxTopRight, Millionär.Properties.Resources.topRightYellow);
                markYellow(labelAn2);
                chooseAnswer = 'B';
            }
        }

        private void labelAn3_Click(object sender, EventArgs e)
        {
            if (unanwsered)
            {
                setAnswerBox(true, true, pictureBoxBottomLeft, Millionär.Properties.Resources.bottomleftYellow);
                markYellow(labelAn3);
                chooseAnswer = 'C';
            }
        }

        private void labelAn4_Click(object sender, EventArgs e)
        {
            if (unanwsered)
            {
                setAnswerBox(true, true, pictureBoxBottomRight, Millionär.Properties.Resources.bottomrightYellow);
                markYellow(labelAn4);
                chooseAnswer = 'D';
            }
        }

        private void NextQuestion()
        {
            //hier irgendwas was den pfeil weiter bewegt
            chooseAnswer = 'E';
            unanwsered = true;
            if (currentQuestion > 1 && currentQuestion < 16)
                ChangePosi(new Control[] { labelArrowLeft, labelArrowRight }, -18);
            q = new QuestionLoader(currentQuestion, lines);
            setAnswerBox(true, false, null, null);
            labelQuestion.Text = q.getQuestion();
            labelAn1.Text = q.getPosAnswer('A');
            labelAn2.Text = q.getPosAnswer('B');
            labelAn3.Text = q.getPosAnswer('C');
            labelAn4.Text = q.getPosAnswer('D');
            labelAn1.Show();
            labelAn2.Show();
            labelAn3.Show();
            labelAn4.Show();
        }

        private void setAnswerBox(bool reset, bool setpicture, PictureBox toset, Image image)
        {
            if (reset)
            {
                pictureBoxTopLeft.Image = Millionär.Properties.Resources.topleft;
                pictureBoxTopRight.Image = Millionär.Properties.Resources.topright;
                pictureBoxBottomLeft.Image = Millionär.Properties.Resources.bottomleft;
                pictureBoxBottomRight.Image = Millionär.Properties.Resources.bottomright;
                labelAn1.BackColor = Color.FromArgb(0, 21, 96);
                labelAn2.BackColor = Color.FromArgb(0, 21, 96);
                labelAn3.BackColor = Color.FromArgb(0, 21, 96);
                labelAn4.BackColor = Color.FromArgb(0, 21, 96);
                labelAn1.ForeColor = Color.White;
                labelAn2.ForeColor = Color.White;
                labelAn3.ForeColor = Color.White;
                labelAn4.ForeColor = Color.White;
            }
            if (setpicture)
                toset.Image = image;
            pictureBoxBackground.Refresh();
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            if(chooseAnswer.Equals('E'))
            {
                if (MessageBox.Show("Du hast keine Frage ausgewählt. Wirklich auflösen?", "Auflösen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            unanwsered = false;
            if (chooseAnswer.Equals(q.getAnswer()))
            {
                switch (chooseAnswer)
                {
                    case 'A':
                        setAnswerBox(false, true, pictureBoxTopLeft, Millionär.Properties.Resources.topleftGreen);
                        markGreen(labelAn1);
                        break;
                    case 'B':
                        setAnswerBox(false, true, pictureBoxTopRight, Millionär.Properties.Resources.topRightGreen);
                        markGreen(labelAn2);
                        break;
                    case 'C':
                        setAnswerBox(false, true, pictureBoxBottomLeft, Millionär.Properties.Resources.bottomleftGreen);
                        markGreen(labelAn3);
                        break;
                    case 'D':
                        setAnswerBox(false, true, pictureBoxBottomRight, Millionär.Properties.Resources.bottomrightGreen);
                        markGreen(labelAn4);
                        break;
                }

                if (currentQuestion == 15)
                {
                    currentQuestion = 16;
                    won = true;
                    buttonEnd.Show();
                }
                else
                {
                    currentQuestion++;
                    buttonNext.Show();
                }
            }
            else
            {
                switch (chooseAnswer)
                {
                    case 'A':
                        setAnswerBox(false, true, pictureBoxTopLeft, Millionär.Properties.Resources.topleftRed);
                        markRed(labelAn1);
                        break;
                    case 'B':
                        setAnswerBox(false, true, pictureBoxTopRight, Millionär.Properties.Resources.topRightRed);
                        markRed(labelAn2);
                        break;
                    case 'C':
                        setAnswerBox(false, true, pictureBoxBottomLeft, Millionär.Properties.Resources.bottomleftRed);
                        markRed(labelAn3);
                        break;
                    case 'D':
                        setAnswerBox(false, true, pictureBoxBottomRight, Millionär.Properties.Resources.bottomrightRed);
                        markRed(labelAn4);
                        break;
                }

                switch (q.getAnswer())
                {
                    case 'A':
                        setAnswerBox(false, true, pictureBoxTopLeft, Millionär.Properties.Resources.topleftGreen);
                        markGreen(labelAn1);
                        break;
                    case 'B':
                        setAnswerBox(false, true, pictureBoxTopRight, Millionär.Properties.Resources.topRightGreen);
                        markGreen(labelAn2);
                        break;
                    case 'C':
                        setAnswerBox(false, true, pictureBoxBottomLeft, Millionär.Properties.Resources.bottomleftGreen);
                        markGreen(labelAn3);
                        break;
                    case 'D':
                        setAnswerBox(false, true, pictureBoxBottomRight, Millionär.Properties.Resources.bottomrightGreen);
                        markGreen(labelAn4);
                        break;
                }
                won = false;
                buttonNext.Hide();
                buttonSolve.Hide();
                buttonStop.Hide();
                buttonStartmenu.Hide();
                buttonEnd.Show();

            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            NextQuestion();
            buttonNext.Hide();
        }

        bool popup = false;
        private void showPopUp(string msg)
        {
            popup = true;
            labelAn1.Hide();
            labelAn2.Hide();
            labelAn3.Hide();
            labelAn4.Hide();
            labelQuestion.Text = msg;
            ChangePosi(new Control[] { pictureBoxTop, labelGewinn, labelQuestion, pictureBoxSide3, pictureBoxSide4 }, 80);
            pictureBoxBackground.Refresh();
        }

        private void ChangePosi(Control[] change, int count)
        {
            foreach(Control tochange in change)
                tochange.Location = new Point(tochange.Location.X, tochange.Location.Y + count);
        }

        private void Ende()
        {
            buttonNext.Hide();
            buttonEnd.Hide();
            buttonSolve.Hide();
            buttonStop.Hide();
            buttonStartmenu.Show();

            if (won)
            {
                if (currentQuestion > 15)
                    showPopUp("€ 1.000.000");
                else
                    showPopUp("€ " + gewinne[currentQuestion - 1]);
            }
            else
            {
                if (currentQuestion > 10)
                    showPopUp("€ 16.000");
                else if (currentQuestion > 5)
                    showPopUp("€ 500");
                else if (currentQuestion < 6)
                    showPopUp("€ 0");
            }
        }

        private void J_Publikum()
        {
            //nichts
        }

        int lastsec = 0;
        bool counter = false;
        private void J_Telefon()
        {
            lastsec = DateTime.Now.Second;
            counter = true;
            pictureBoxBackground.Refresh();
            labelCounter.Show();
            Telefon.Start();
        }

        private void Telefon_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Second != lastsec)
            {
                labelCounter.Text = "" + (Convert.ToInt32(labelCounter.Text) - 1);
                lastsec = DateTime.Now.Second;
            }
            if (labelCounter.Text.Equals("0"))
            {
                labelCounter.Hide();
                counter = false;
                pictureBoxBackground.Refresh();
                Telefon.Stop();
            }
        }

        private void J_5050()
        {
            char answer = q.getAnswer();
            if (answer == 'E')
                return;
            int pickanswer1 = new Random().Next(0, 3);
            int pickanswer2 = new Random().Next(0, 3);
            while(pickanswer1 == pickanswer2)
                pickanswer2 = new Random().Next(0, 3);
            Label[] posDelete = new Label[3];

            switch (answer)
            {
                case 'A':
                    posDelete[0] = labelAn2;
                    posDelete[1] = labelAn3;
                    posDelete[2] = labelAn4;
                    break;
                case 'B':
                    posDelete[0] = labelAn1;
                    posDelete[1] = labelAn3;
                    posDelete[2] = labelAn4;
                    break;
                case 'C':
                    posDelete[0] = labelAn1;
                    posDelete[1] = labelAn2;
                    posDelete[2] = labelAn4;
                    break;
                case 'D':
                    posDelete[0] = labelAn1;
                    posDelete[1] = labelAn2;
                    posDelete[2] = labelAn3;
                    break;
            }
            posDelete[pickanswer1].Hide();
            posDelete[pickanswer2].Hide();

        }

        private bool JokerMessageBox(ref bool joker, string jokername)
        {
            if (joker)
            {
                if (MessageBox.Show(jokername + " Joker einsetzen", "Joker einsetzen?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    joker = false;
                    return true;
                }
            }
            else
                MessageBox.Show("Dieser Joker wurde bereits verwendet!", "Joker verwendet!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        private void pictureBoxBackground_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
                contextMenuMain.Show(Cursor.Position);

            if(e.Button.Equals(MouseButtons.Left) && clickjoker(pictureBox5050, e))
                if (JokerMessageBox(ref _5050, "50:50"))
                    J_5050();

            if (e.Button.Equals(MouseButtons.Left) && clickjoker(pictureBoxTelefon, e))
                if (JokerMessageBox(ref telefon, "Telefon"))
                    J_Telefon();
            
            if (e.Button.Equals(MouseButtons.Left) && clickjoker(pictureBoxPublikum, e))
                if (JokerMessageBox(ref publikum, "Publikum"))
                    J_Publikum();		
        }

        private bool clickjoker(PictureBox joker, MouseEventArgs e)
        {
            return clickArea(
                joker.Location.X, 
                joker.Location.X + joker.Size.Width, 
                joker.Location.Y,
                joker.Location.Y + joker.Size.Height, 
                e.Location);
        }

        private void vollbildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void hauptmenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Möchtest du zum Hauptmenü zurückkehren? Das derzeitige Spiel wird dadurch beendet!", "Sicher?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                intro.Show();
                Close();
            }
        }

        private void minimierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Minimized;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Möchtest du das derzeitige Spiel wirklich beenden?", "usure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                intro.Close();
        }

        private void pictureBoxBackground_Paint(object sender, PaintEventArgs e)
        {
            if (!popup)
            {
                //small sidebars
                draw(e.Graphics, pictureBoxSide1);
                draw(e.Graphics, pictureBoxSide2);
                draw(e.Graphics, pictureBoxSide5);
                draw(e.Graphics, pictureBoxSide6);
                //anwser boxes
                draw(e.Graphics, pictureBoxTopRight);
                draw(e.Graphics, pictureBoxTopLeft);
                draw(e.Graphics, pictureBoxBottomLeft);
                draw(e.Graphics, pictureBoxBottomRight);
                //letters
                drawText(e.Graphics, labelA);
                drawText(e.Graphics, labelB);
                drawText(e.Graphics, labelC);
                drawText(e.Graphics, labelD);
            }
            else
            {
                //gewinnsumme
                drawText(e.Graphics, labelGewinn);
            }
           
            //big sidebars
            draw(e.Graphics, pictureBoxSide3);
            draw(e.Graphics, pictureBoxSide4);
            //Question
            draw(e.Graphics, pictureBoxTop);
            //euro sidebar
            draw(e.Graphics, pictureBoxEuros);
            //joker backgrounds
            draw(e.Graphics, pictureBoxPublikum);
            draw(e.Graphics, pictureBox5050);
            draw(e.Graphics, pictureBoxTelefon);
            //joker
            draw(e.Graphics, pictureBoxJoker);
            draw(e.Graphics, pictureBoxJoker2);
            draw(e.Graphics, pictureBoxJoker3);
            //telefon counter
            if(counter)
                draw(e.Graphics, pictureBoxCountdown);
            if (map != null)
            {
                map.Dispose();
                map = null;
            }
            GC.Collect();
        }

        private bool clickArea(int left, int right, int top, int bottom, Point mousePos)
        {
            if (mousePos.X >= left && mousePos.X <= right && mousePos.Y >= top && mousePos.Y <= bottom)
                return true;
            return false;
        }

        private void draw(Graphics g, PictureBox todraw)
        {
            g.DrawImage(resize(todraw.Image, todraw.Size), todraw.Location);
        }

        private void drawText(Graphics g, Label todraw)
        {
            g.DrawString(todraw.Text, todraw.Font, new SolidBrush(todraw.ForeColor), todraw.Location);
        }

        Bitmap map;
        private Bitmap resize(Image resize, Size p)
        {
            try
            {
                map = new Bitmap((Bitmap)resize, p.Width, p.Height);
            }
            catch (Exception) { map = new Bitmap(1, 1); }
            return map;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Wirklich aufhören?", "Aufhören?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Ende();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ende();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            intro.Show();
            Close();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!intro.Visible)
                intro.Close();
        }

    }
}
