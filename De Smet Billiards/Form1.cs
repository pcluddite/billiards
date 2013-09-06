using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace De_Smet_Billiards {
    public partial class Form1 : Form {

        XmlDocument doc = new XmlDocument();
        public List<Player> players = new List<Player>();
        public List<int> usedIDs = new List<int>();
        public Random r = new Random();
        public int completed;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            try {
                doc.Load(Application.StartupPath + "\\Players.xml");

                completed = int.Parse(doc.SelectSingleNode("players/gamescompleted").Attributes["m"].Value);

                foreach (XmlNode player in doc.SelectNodes("players/player")) {


                    int id;
                    while (usedIDs.Contains((id = r.Next()))) {
                    }

                    players.Add(new Player(id, 
                        player.Attributes["name"].Value, int.Parse(player.Attributes["rating"].Value),
                        int.Parse(player.Attributes["wins"].Value), int.Parse(player.Attributes["losses"].Value)));

                    checkedListBox1.Items.Add(player.Attributes["name"].Value);
                    checkedListBox2.Items.Add(player.Attributes["name"].Value);
                }
            }
            catch {
                doc.LoadXml("<players></players>");
                checkedListBox1.Items.Clear();
                checkedListBox2.Items.Clear();
                players.Clear();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                doc.SelectSingleNode("players").RemoveAll();
                foreach(var v in players) {
                    XmlElement player = doc.CreateElement("player");
                    player.SetAttribute("name", v.Name);
                    player.SetAttribute("rating", v.Rating.ToString());
                    player.SetAttribute("wins", v.Won.ToString());
                    player.SetAttribute("losses", v.Lost.ToString());
                    doc.SelectSingleNode("players").AppendChild(player);
                }
                XmlElement gc = doc.CreateElement("gamescompleted");
                gc.SetAttribute("m", completed.ToString());
                doc.SelectSingleNode("players").AppendChild(gc);
                doc.Save(Application.StartupPath + "\\Players.xml");
            }
            catch (Exception ex) {
                if (MessageBox.Show(this, "A problem occoured that may prevent the saving of the Rankings file!\r\nThe error is:\r\n" +
                    ex.Message + "\r\n\r\nDo you still want to close? (All data from this session will be lost)", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.No) {
                        e.Cancel = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            New addNew = new New(this);
            addNew.ShowDialog(this);

            reset();
        }

        void reset() {
            string name1 = null, name2 = null;
            if (checkedListBox1.CheckedItems.Count > 0) {
                name1 = (string)checkedListBox1.CheckedItems[0];
            }
            if (checkedListBox2.CheckedItems.Count > 0) {
                name2 = (string)checkedListBox2.CheckedItems[0];
            }
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            foreach (var v in players) {
                checkedListBox1.Items.Add(v.Name, (name1 == v.Name));
                checkedListBox2.Items.Add(v.Name, (name2 == v.Name));
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            this.Hide();
            Rankings r = new Rankings(this);
            r.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (!check())
                return;

            Player player1 = new Player(0, "", 0, 0, 0);
            Player player2 = new Player(0, "", 0, 0, 0);
            foreach (Player p in players) {
                if (p.Name == (string)checkedListBox1.CheckedItems[0]) {
                    player1 = p;
                }
                if (p.Name == (string)checkedListBox2.CheckedItems[0]) {
                    player2 = p;
                }
            }
            int whoWon = (sender == (object)button1) ? 1 : 2;
            Compete c = new Compete(player1, player2, whoWon, this);
            if (c.ShowDialog() == DialogResult.Cancel)
                return;
            
            reset();
        }

        bool check() {

            if (checkedListBox1.CheckedItems.Count == 0) {
                MessageBox.Show(this, "You must select a first player!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkedListBox1.Select();
                return false;
            }
            if (checkedListBox2.CheckedItems.Count == 0) {
                MessageBox.Show(this, "You must select a second player!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkedListBox2.Select();
                return false;
            }
            if (checkedListBox1.CheckedItems[0] == checkedListBox2.CheckedItems[0]) {
                MessageBox.Show(this, "You cannot have a player compete against himself!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkedListBox1.Select();
                return false;
            }
            return true;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (e.NewValue == CheckState.Checked) {
                for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                    if (e.Index != ix) checkedListBox1.SetItemChecked(ix, false);
            }
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (e.NewValue == CheckState.Checked) {
                for (int ix = 0; ix < checkedListBox2.Items.Count; ++ix)
                    if (e.Index != ix) checkedListBox2.SetItemChecked(ix, false);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            Remove r = new Remove(this);
            r.ShowDialog();
            reset();
        }
    }
}
