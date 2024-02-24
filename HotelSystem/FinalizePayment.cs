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
    public partial class FinalizePayment : Form
    {
        public FinalizePayment()
        {
            InitializeComponent();
        }

        public string PAYMENTTYPE
        {
            get => comboBoxPaymentType.Text;
        }

        public string CARDEXP
        {
            get => comboBoxmonth.Text + "/" +comboBoxyear.Text;
        }

        public string CVC
        {
            get => textBoxCVC.Text;
        }

        public string CARDNUMBER
        {
            get => textBoxCardNumber.Text;
        }

        public string CARDTYPE
        {
            get => labelCardType.Text;
        }

        public void get_check(Reservation R)
        {
            comboBoxPaymentType.DataBindings.Add("Text", R, "PaymentType");
            comboBoxmonth.DataBindings.Add("Text", R, "CARDEXPMONTH");
            comboBoxyear.DataBindings.Add("Text", R, "CARDEXPYEAR");
            textBoxCVC.DataBindings.Add("Text", R, "CardCvc");
            textBoxCardNumber.DataBindings.Add("Text", R, "CardNumber");
            labelCardType.Text = R.CardType;

            labelCurrentBill.Text += R.TotalBill;
            labelFoodBill.Text += R.FoodBill;
            labelTax.Text += R.TotalBill * 0.07;
            double total = R.TotalBill + R.TotalBill * 0.07 + R.FoodBill;
            labelTotal.Text += total;
            
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Payment_Blank())
            {
                MessageBox.Show("Field cannot be Blank");
            }
            else
                this.Hide();
        }

        private void textBoxCardNumber_Leave(object sender, EventArgs e)
        {
            if (textBoxCardNumber.Text.Substring(0, 1) == "3")
            {
                labelCardType.Text = "AmericanExpress";
            }
            else if (textBoxCardNumber.Text.Substring(0, 1) == "4")
            {
                labelCardType.Text = "Visa";
            }
            else if (textBoxCardNumber.Text.Substring(0, 1) == "5")
            {
                labelCardType.Text = "MasterCard";
            }
            else if (textBoxCardNumber.Text.Substring(0, 1) == "6")
            {
                labelCardType.Text = "Discover";
            }
        }

        public bool Payment_Blank()
        {
            if (string.IsNullOrEmpty(textBoxCardNumber.Text) || (string.IsNullOrEmpty(textBoxCVC.Text))
                || string.IsNullOrEmpty(comboBoxPaymentType.Text) || string.IsNullOrEmpty(comboBoxmonth.Text)
                || string.IsNullOrEmpty(comboBoxyear.Text))
            {
                return true;
            }
            return false;
        }
    }
}
