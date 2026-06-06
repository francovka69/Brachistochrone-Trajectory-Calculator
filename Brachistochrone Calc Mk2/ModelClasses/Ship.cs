using System;
using System.Collections.Generic;
using System.Text;

namespace Brachistochrone_Calc_Mk2.ModelClasses
{
    public class Ship
    {
        string[] NameRandom = new string[] {"Rocinante-", "Cook-", "C-", "Enterprise-", "Endevour-", "Endurance-", "Eternity's Advent-",
        "Proximity-", "A-", "Bob-", "Jimmy-", "EFD-", "CFI-", "UNN-", "Tachi-", "Orville-", "Icarus Of Heaven-", "Rocinante-",
        "Venator-", "MAO-", "MCRN-", "OPA-", "LAC-", "Carlo-", "Carlo-", "Roddos-", "Osprey-", "HMS-", "Pelican", "HALO-",
        "Titan-", "Firefly-", "Goontube-", "Goontube-", "Orion-", "Shitfuck-"};
        public string input { get; set; }

        public string Name;


        Random shabbatshallom = new Random();

        public Ship(string input)
        {
            this.Name = input;
        }

        public Ship()
        {

        }




        public void Generate()
        {
            
            int randomnumber = shabbatshallom.Next(1, 1000);
            string RNString = randomnumber.ToString();
            Name = NameRandom[shabbatshallom.Next(1, 17)] + RNString;
            

        }

        public override string ToString()
        {
            return $"The ship is named {Name}";
        }
    }
}
