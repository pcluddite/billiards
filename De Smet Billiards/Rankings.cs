using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace De_Smet_Billiards {
    public partial class Rankings : Form {
        Form1 form;
        public Rankings(Form1 form) {
            this.form = form;
            InitializeComponent();
        }

        private void Rankings_Load(object sender, EventArgs e) {
            List<KeyValuePair<int, int>> ranks = new List<KeyValuePair<int, int>>();
            Dictionary<int, Player> players = new Dictionary<int, Player>();
            
            foreach (Player p in form.players) {
                ranks.Add(new KeyValuePair<int, int>(p.ID, p.Rating));
                players.Add(p.ID, p);
            }

            ranks.Sort(
                delegate(KeyValuePair<int, int> firstPair,
                    KeyValuePair<int, int> nextPair) {
                    return firstPair.Value.CompareTo(nextPair.Value);
                }
            );
            ranks.Reverse();

            int ix = 1;
            for (int i = 0; i < ranks.Count; i++) {
                ListViewItem item = new ListViewItem();
                item.Text = ix.ToString();
                item.SubItems.Add(players[ranks[i].Key].Name);
                item.SubItems.Add(players[ranks[i].Key].Won.ToString());
                item.SubItems.Add(players[ranks[i].Key].Lost.ToString());
                item.SubItems.Add(players[ranks[i].Key].Rating.ToString());
                listView1.Items.Add(item);
                ix++;
                if (i < ranks.Count - 1 && ranks[i].Value == ranks[i + 1].Value)
                    ix--;
            }
        }
    }
}