using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace De_Smet_Billiards {
    public partial class Compete : Form {

        Player player1, player2;
        int winning;
        Form1 form;

        public Compete(Player player1, Player player2, int winningPlayer, Form1 form) {
            InitializeComponent();
            this.player1 = player1;
            this.player2 = player2;
            winning = winningPlayer;
            this.form = form;
        }

        private void Compete_Load(object sender, EventArgs e) {
            if (winning == 1) {
                Text = player1.Name + " defeats " + player2.Name;
            }
            else {
                Text = player2.Name + " defeats " + player1.Name;
            }
            label1.Text = player1.Name;
            label2.Text = player2.Name;
            if (winning == 1) {
                player1.Won++;
                player2.Lost++;
            }
            else {
                player2.Won++;
                player1.Lost++;
            }
            textBox1.Text = player1.Rating.ToString();
            textBox2.Text = player2.Rating.ToString();
            textBox3.Text = getScorePA(player1, player2).ToString();
            textBox4.Text = getScorePB(player1, player2).ToString();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        public decimal chanceOfA(int ra, int rb) {
            return 1 / (decimal)(1 + Math.Pow(10, (double)((rb - ra) / 400)));
        }

        public decimal chanceOfB(int ra, int rb) {
            return 1 / (decimal)(1 + Math.Pow(10, (double)((ra - rb) / 400)));
        }

        public int getScorePA(Player pa, Player pb) {
            decimal exp = chanceOfA(pa.Rating, pb.Rating);
            int win = (winning == 1) ? 1 : 0;
            return (int)(pa.Rating + (32 * (decimal)(win - exp)));
        }

        public int getScorePB(Player pa, Player pb) {
            decimal exp = chanceOfB(pa.Rating, pb.Rating);
            int win = (winning == 1) ? 0 : 1;
            return (int)(pb.Rating + (32 * (decimal)(win - exp)));
        }

        private void button1_Click(object sender, EventArgs e) {
            Player p1 = new Player(player1.ID, player1.Name, getScorePA(player1, player2), player1.Won, player1.Lost);
            Player p2 = new Player(player2.ID, player2.Name, getScorePB(player1, player2), player2.Won, player2.Lost);
            int i = 0;
            Player[] nList = new Player[form.players.Count];
            form.players.CopyTo(nList);
            foreach (Player p in nList) {
                if (p.Name == p1.Name) {
                    form.players[i] = p1;
                }
                if (p.Name == p2.Name) {
                    form.players[i] = p2;
                }
                i++;
            }
            Hide();
            MessageBox.Show(this, p1.Name + "'s rating has been set to " + p1.Rating + ",\r\nand " +
                p2.Name + "'s rating is now " + p2.Rating + ".", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            form.completed++;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
