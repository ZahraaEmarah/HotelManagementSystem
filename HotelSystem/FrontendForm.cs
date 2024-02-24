using Dapper;
using HotelSystem.ReservationContext;
using HotelSystem.ReservationEntities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem
{
    public partial class FrontendForm : Form
    {
        FRONTEND_RESERVATIONContext ResContext;
        List<Reservation> reservations;
        bool loaded = false;
        InsertForm N;
        public FrontendForm()
        {
            InitializeComponent();
            ResContext = new FRONTEND_RESERVATIONContext();
            this.FormClosed += (sender, e) => ResContext?.Dispose();
        }

        private void FrontendForm_Load(object sender, EventArgs e)
        {
            ResContext.Reservation.Load();
            PopulateDOBcomboboxes();

            var OccupiedOrReservedRooms = ResContext.Reservation.Select(P => P.RoomNumber.Trim()).ToList();
            foreach (var item in OccupiedOrReservedRooms)
                roomNoComboBox.Items.Remove(item);

            dataGridView1.DataSource = ResContext.Reservation.Local.ToBindingList();
            dataGridView1.Columns["MONTH"].Visible = false;
            dataGridView1.Columns["DAY"].Visible = false;
            dataGridView1.Columns["YEAR"].Visible = false;
            dataGridView1.Columns["CARDEXPMONTH"].Visible = false;
            dataGridView1.Columns["CARDEXPYEAR"].Visible = false;
            var Occupiedrooms = (from o in ResContext.Reservation
                                 where o.CheckIn == true
                                 select o.OccupiedRoomToString()).ToList();

            listBoxOccupied.DataSource = Occupiedrooms;

            var Reserveddrooms = (from o in ResContext.Reservation
                                 where o.CheckIn == false
                                 select o.ReservedRoomToString()).ToList();

            listBoxReserved.DataSource = Reserveddrooms;

            
            //SqlConnection SqlCn = new SqlConnection("Data Source=.; Initial Catalog=FRONTEND_RESERVATION; Integrated Security=true;");
            //reservations = SqlCn.Query<Reservation>("Select * From reservation")?.ToList()??new();
            //foreach(var item in reservations)
            //{
            //    Trace.WriteLine(item.BirthDay);
            //}
            reservations = ResContext.Reservation.Select(c => c).ToList();
        }

        private void f2_getP(object sender, EventArgs e)
        {
            Reservation R = N.getEntry();
            if (!R.Cleaning && !R.Towel && !R.SSurprise && R.BreakFast == 0 && R.Lunch == 0 && R.Dinner == 0)
                R.SupplyStatus = true;
            ResContext.Add(R);
            ResContext.SaveChanges();
            MessageBox.Show("Inserted Successfully");
            
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditcomboBox.Visible = true;
            EditLabel.Visible = true;
            UpdateBtn.Visible = true;
            DeleteBtn.Visible = true;
            loaded = true;
            EditcomboBox.DataSource = reservations;
            firstnametxt.DataBindings.Add("Text", reservations, "FirstName");
            lastnametxt.DataBindings.Add("Text", reservations, "LastName");
            comboBoxDay.DataBindings.Add("Text", reservations, "DAY");
            comboBoxMonth.DataBindings.Add("Text", reservations, "MONTH");
            comboBoxYear.DataBindings.Add("Text", reservations, "YEAR");
            GendercomboBox.DataBindings.Add("Text", reservations, "Gender");
            textBoxPhone.DataBindings.Add("Text", reservations, "PhoneNumber");
            textBoxEmail.DataBindings.Add("Text", reservations, "EmailAddress");
            textBoxStreet.DataBindings.Add("Text", reservations, "StreetAddress");
            textBoxApt.DataBindings.Add("Text", reservations, "AptSuite");
            textBoxCity.DataBindings.Add("Text", reservations, "City");
            comboBoxState.DataBindings.Add("Text", reservations, "State");
            textBoxZipCode.DataBindings.Add("Text", reservations, "ZipCode");

            NoOfGuestscomboBox.DataBindings.Add("Text", reservations, "NumberGuest");
            RoomTypecomboBox.DataBindings.Add("Text", reservations, "RoomType");
            FloorcomboBox.DataBindings.Add("Text", reservations, "RoomFloor");
            roomNoComboBox.DataBindings.Add("Text", reservations, "RoomNumber");

            EntrydateTimePicker.DataBindings.Add("Text", reservations, "ArrivalTime");
            DeparturedateTimePicker.DataBindings.Add("Text", reservations, "LeavingTime");

            checkBoxCheckIn.DataBindings.Add("Checked", reservations, "CheckIn");
            checkBoxFood.DataBindings.Add("Checked", reservations, "SupplyStatus");
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (!Is_Blank())
            {
                reservations[EditcomboBox.SelectedIndex].BirthDay =
                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(comboBoxMonth.Text))
                    + "-" + comboBoxDay.Text + "-" + comboBoxYear.Text;
                ResContext.SaveChanges();
                MessageBox.Show("Updated Successfully");
            }
            else
            {
                MessageBox.Show("Field cannot be Blank");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            ResContext.Remove(EditcomboBox.SelectedItem);
            EditcomboBox.SelectedItem = reservations[0];
            ResContext.SaveChanges();
            MessageBox.Show("Deleted Successfully");
        }

        private void FoodBtn_Click(object sender, EventArgs e)
        {
            if (loaded)
            {
                FoodMenu f2 = new FoodMenu();

                f2.Bind_Breakfast((Reservation)EditcomboBox.SelectedItem);
                f2.Show();
            }
            else
            {
                MessageBox.Show("You must select a guest first!");
            }       
        }

        private void BillBtn_Click(object sender, EventArgs e)
        {
            if (loaded)
            {
                FinalizePayment f = new FinalizePayment();
                f.get_check((Reservation)EditcomboBox.SelectedItem);
                f.Show();
            }
            else
            {
                MessageBox.Show("You must select a guest first!");
            }
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            N = new InsertForm(ResContext.Reservation.Select(P => P.RoomNumber.Trim()).ToList());
            N.AddClicked += f2_getP;
            N.Show();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            int id = 0;
            bool valid = int.TryParse(textBoxSearch.Text, out id);
            Trace.WriteLine(id);

            var SearchResult = (from S in ResContext.Reservation
                                where S.Id.Equals(id) || S.FirstName.Contains(textBoxSearch.Text)
                                || S.LastName.Contains(textBoxSearch.Text)
                                || S.Gender.Equals(textBoxSearch.Text)
                                || S.State.Contains(textBoxSearch.Text)
                                || S.City.Contains(textBoxSearch.Text)
                                || S.RoomNumber.Contains(textBoxSearch.Text)
                                || S.RoomType.Contains(textBoxSearch.Text)
                                || S.EmailAddress.Contains(textBoxSearch.Text)
                                || S.PhoneNumber.Contains(textBoxSearch.Text)
                                select S).ToList();

            dataGridViewSearch.DataSource = SearchResult;
            dataGridViewSearch.Show();
            dataGridViewSearch.Columns["MONTH"].Visible = false;
            dataGridViewSearch.Columns["DAY"].Visible = false;
            dataGridViewSearch.Columns["YEAR"].Visible = false;
            dataGridViewSearch.Columns["CARDEXPMONTH"].Visible = false;
            dataGridViewSearch.Columns["CARDEXPYEAR"].Visible = false;
        }

        private void PopulateDOBcomboboxes()
        {
            var DaysDataSource = Enumerable.Range(1, 31).ToList();
            comboBoxDay.DataSource = DaysDataSource;
            var MonthsDataSource = Enumerable.Range(1, 12).ToList();
            comboBoxMonth.DataSource = MonthsDataSource;
            var YearsDataSource = Enumerable.Range(((DateTime.Today.Year) - 100), 82).ToList();
            comboBoxYear.DataSource = YearsDataSource;
        }

        private bool Is_Blank()
        {
            if (string.IsNullOrEmpty(firstnametxt.Text) || (string.IsNullOrEmpty(lastnametxt.Text))
                || string.IsNullOrEmpty(comboBoxDay.Text) || string.IsNullOrEmpty(comboBoxMonth.Text)
                || string.IsNullOrEmpty(comboBoxYear.Text) || string.IsNullOrEmpty(GendercomboBox.Text)
                || string.IsNullOrEmpty(textBoxPhone.Text) || string.IsNullOrEmpty(textBoxEmail.Text)
                || string.IsNullOrEmpty(textBoxStreet.Text) || string.IsNullOrEmpty(textBoxApt.Text)
                || string.IsNullOrEmpty(textBoxCity.Text) || string.IsNullOrEmpty(comboBoxState.Text)
                || string.IsNullOrEmpty(textBoxZipCode.Text) || string.IsNullOrEmpty(NoOfGuestscomboBox.Text)
                || string.IsNullOrEmpty(RoomTypecomboBox.Text) || string.IsNullOrEmpty(roomNoComboBox.Text)
                || string.IsNullOrEmpty(FloorcomboBox.Text))
            {
                return true;
            }
            return false;
        }
    }
}
