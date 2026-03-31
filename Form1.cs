using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            //these are the icons we will be using in our game, each icon appears twice cause we need to find the pairs of icons
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        Label firstClicked, secondClicked;

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void label_click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked!=null)
                return;  //to stopping user for clicking more than 2 boxes

            Label clickedlabel = sender as Label;

            if (clickedlabel == null) 
                return;

            if(clickedlabel.ForeColor == Color.Black)
                return;

            //if the code reaches this point, it means the user has clicked a label that is not currently revealed
            if (firstClicked == null)
            {
                firstClicked = clickedlabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            //second click
            secondClicked = clickedlabel;
            secondClicked.ForeColor = Color.Black;

            //check if the user won
            checkforwinner();

            //if the user gets this far, the player has clicked two different icons, so start the timer
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                timer1.Start();
            }
        }
        private void checkforwinner()
        {
            Label label;
            //check every label in the table layout panel to see if any of them are not matched (if their forecolor is the same as their backcolor, then they are not matched)
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                {
                    return;
                }
            }


            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            //hide both icons
            firstClicked.ForeColor=firstClicked.BackColor;
            secondClicked.ForeColor=secondClicked.BackColor;

            //reset firstClicked and secondClicked so the user can click another icon
            firstClicked = null;
            secondClicked = null;

        }

        private void AssignIconsToSquares()
        {
            Label label;
            int randomNumber;

            //assign each icon from the list of icons to a random square in the table layout panel
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                //if the control is a label, then assign an icon to it
                if (tableLayoutPanel1.Controls[i] is Label)
                    label = (Label)tableLayoutPanel1.Controls[i];
                else
                    continue;

                //generate a random number between 0 and the number of icons left in the list
                randomNumber = random.Next(0, icons.Count);
                label.Text = icons[randomNumber];

                icons.RemoveAt(randomNumber);
            }
        }
    }  
}