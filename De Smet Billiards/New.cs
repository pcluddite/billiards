using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace De_Smet_Billiards {
    public partial class New : Form {
        
        Form1 form;
        public List<string> players = new List<string>();

        public New(Form1 form) {
            InitializeComponent();
            this.form = form;
            foreach (Player p in form.players) {
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (players.Contains(textBox1.Text)) {
                MessageBox.Show(this, "You already have a player with this name!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                int id;
                while (form.usedIDs.Contains(id = form.r.Next())) {
                }
                form.players.Add(new Player(id, textBox1.Text, 500, 0, 0));
                this.Close();
            }
        }
    }
}
