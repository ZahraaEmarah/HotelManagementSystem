using HotelSystem.ReservationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem
{
    public partial class FoodMenu : Form
    {
        public FoodMenu()
        {
            InitializeComponent();
        }

        private int lunchQ = 0;

        public int LunchQ
        {
            get { return lunchQ; }
            set { lunchQ = value; }
        }
        private int breakfastQ = 0;

        public int BreakfastQ
        {
            get { return breakfastQ; }
            set { breakfastQ = value; }
        }
        private int dinnerQ = 0;

        public int DinnerQ
        {
            get { return dinnerQ; }
            set { dinnerQ = value; }
        }

        private bool cleaning = false;

        public bool Cleaning
        {
            get { return checkBoxCleaning.Checked; }
            set { cleaning = value; }
        }
        private bool towel = false;

        public bool Towel
        {
            get { return checkBoxTowels.Checked; }
            set { towel = value; }
        }

        private bool surprise = true;

        public bool Surprise
        {
            get { return checkBoxSurprise.Checked; }
            set { surprise = value; }
        }

        public void Bind_Breakfast(Reservation src)
        {
            checkBoxBreakfast.DataBindings.Add("Checked", src, "BreakFast");
            checkBoxLunch.DataBindings.Add("Checked", src, "Lunch");
            checkBoxDinner.DataBindings.Add("Checked", src, "Dinner");

            checkBoxCleaning.DataBindings.Add("Checked", src, "Cleaning");
            checkBoxTowels.DataBindings.Add("Checked", src, "Towel");
            checkBoxSurprise.DataBindings.Add("Checked", src, "SSurprise");

            textBoxBreakfast.DataBindings.Add("Text", src, "BreakFast");
            textBoxLunch.DataBindings.Add("Text", src, "Lunch");
            textBoxDinner.DataBindings.Add("Text", src, "Dinner");
        }

        private void FoodOkayBtn_Click_1(object sender, EventArgs e)
        {
            if (checkBoxBreakfast.Checked)
            {
                try
                {
                    BreakfastQ = Convert.ToInt32(textBoxBreakfast.Text);
                }
                catch
                {
                    BreakfastQ = 1;
                }
            }
            if (checkBoxLunch.Checked)
            {
                try
                {
                    LunchQ = Convert.ToInt32(textBoxBreakfast.Text);
                }
                catch
                {
                    LunchQ = 1;
                }
            }
            if (checkBoxDinner.Checked)
            {
                try
                {
                    DinnerQ = Convert.ToInt32(textBoxBreakfast.Text);
                }
                catch
                {
                    DinnerQ = 1;
                }
            }

            Cleaning = checkBoxCleaning.Checked;
            Towel = checkBoxTowels.Checked;
            Surprise = checkBoxSurprise.Checked;
            this.Hide();
        }
    }
}
