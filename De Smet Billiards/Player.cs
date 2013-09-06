using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De_Smet_Billiards {
    public class Player {
        public Player(int ID, string Name, int Rating, int Won, int Lost) {
            this.ID = ID;
            this.Name = Name;
            this.Rating = Rating;
            this.Won = Won;
            this.Lost = Lost;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int Rating { get; set; }
    }
}
