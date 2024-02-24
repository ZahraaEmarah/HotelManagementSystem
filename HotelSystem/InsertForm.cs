using HotelSystem.ReservationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem
{
    public partial class InsertForm : Form
    {
        FoodMenu f;
        FinalizePayment p;
        public event EventHandler AddClicked;

        protected virtual void OnAddClicked(EventArgs e)
        {
            EventHandler handler = AddClicked;
            handler?.Invoke(this, e);
        }

        public InsertForm(List<string> OccupiedOrReservedRooms)
        {
            InitializeComponent();
            var DaysDataSource = Enumerable.Range(1, 31).ToList();
            comboBoxDay.DataSource = DaysDataSource;
            var MonthsDataSource = Enumerable.Range(1, 12).ToList();
            comboBoxMonth.DataSource = MonthsDataSource;
            var YearsDataSource = Enumerable.Range(((DateTime.Today.Year) - 100), 82).ToList();
            comboBoxYear.DataSource = YearsDataSource;
            p = new FinalizePayment();
            f = new FoodMenu();
            foreach (var item in OccupiedOrReservedRooms)
                roomNoComboBox.Items.Remove(item);
        }

        private void FoodBtn_Click(object sender, EventArgs e)
        {
            f.Show();
        }

        public Reservation getEntry()
        {
            Reservation R = new Reservation();

            R.FirstName = firstnametxt.Text;
            R.LastName = lastnametxt.Text;
            R.BirthDay = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(comboBoxMonth.Text))
                + "-" + comboBoxDay.Text + "-" + comboBoxYear.Text;
            R.Gender = GendercomboBox.Text;
            R.PhoneNumber = textBoxPhone.Text;
            R.EmailAddress = textBoxEmail.Text;
            int dummy = 0;
            bool valid = int.TryParse(NoOfGuestscomboBox.Text, out dummy);
            R.NumberGuest = dummy;
            R.StreetAddress = textBoxStreet.Text;
            R.AptSuite = textBoxApt.Text;
            R.City = textBoxCity.Text;
            R.State = comboBoxState.Text;
            R.ZipCode = textBoxZipCode.Text;
            R.RoomType = RoomTypecomboBox.Text;
            R.RoomFloor = FloorcomboBox.Text;
            R.RoomNumber = roomNoComboBox.Text;
            R.PaymentType = p.PAYMENTTYPE;
            R.CardType = p.CARDTYPE;
            R.CardNumber = p.CARDNUMBER;
            R.CardExp = p.CARDEXP;
            R.CardCvc = p.CVC;
            R.ArrivalTime = EntrydateTimePicker.Value;
            R.LeavingTime = DeparturedateTimePicker.Value;
            R.CheckIn = checkBoxCheckIn.Checked;
            R.BreakFast = f.BreakfastQ;
            R.Lunch = f.LunchQ;
            R.Dinner = f.DinnerQ;
            R.Cleaning = f.Cleaning;
            R.Towel = f.Towel;
            R.SSurprise = f.Surprise;

            return R;
        }

        private void BillBtn_Click(object sender, EventArgs e)
        {
            p.ShowDialog();
        }

        private void SubmitBtn_Click_1(object sender, EventArgs e)
        {
            if (!Is_Blank())
            {
                OnAddClicked(new());
                Hide();
            }
            else
            {
                MessageBox.Show("Field cannot be Blank");
            }
        }

        private bool Is_Blank()
        {
            if (string.IsNullOrEmpty(lastnametxt.Text) || (string.IsNullOrEmpty(firstnametxt.Text))
                || string.IsNullOrEmpty(comboBoxDay.Text) || string.IsNullOrEmpty(comboBoxMonth.Text)
                || string.IsNullOrEmpty(comboBoxYear.Text) || string.IsNullOrEmpty(GendercomboBox.Text)
                || string.IsNullOrEmpty(textBoxPhone.Text) || string.IsNullOrEmpty(textBoxEmail.Text)
                || string.IsNullOrEmpty(textBoxStreet.Text) || string.IsNullOrEmpty(textBoxApt.Text)
                || string.IsNullOrEmpty(textBoxCity.Text) || string.IsNullOrEmpty(comboBoxState.Text)
                || string.IsNullOrEmpty(textBoxZipCode.Text) || string.IsNullOrEmpty(NoOfGuestscomboBox.Text)
                || string.IsNullOrEmpty(RoomTypecomboBox.Text) || string.IsNullOrEmpty(roomNoComboBox.Text)
                || string.IsNullOrEmpty(FloorcomboBox.Text)
                || p.Payment_Blank())
            {
                return true;
            }
            return false;
        }
    }
}
