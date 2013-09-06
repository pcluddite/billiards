using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace De_Smet_Billiards {
    public partial class Remove : Form {
        Form1 form;
        public Remove(Form1 form) {
            InitializeComponent();
            this.form = form;
        }

        private void Remove_Load(object sender, EventArgs e) {
            foreach (Player p in form.players) {
                checkedListBox1.Items.Add(p.Name);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (MessageBox.Show(this, "Are you sure you want to delete the selected players?", Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                    return;
            }
            Player[] nList = new Player[form.players.Count];
            form.players.CopyTo(nList);

            foreach (Player p in nList) {
                if (checkedListBox1.CheckedItems.Contains(p.Name)) {
                    form.players.Remove(p);
                }
            }
            this.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e) {
            button1.Enabled = !(checkedListBox1.CheckedItems.Count == 0);
        }
    }
}
