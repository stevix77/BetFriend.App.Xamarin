using Syncfusion.SfPicker.XForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;
using SelectionChangedEventArgs = Syncfusion.SfPicker.XForms.SelectionChangedEventArgs;

namespace BetFriend.MobileApp.Controls
{
    public class DateTimePicker : SfPicker
    {
        #region Public Properties

        // Months api is used to modify the Day collection as per change in Month

        internal Dictionary<string, string> Months { get; set; }

        /// <summary>
        /// Date is the acutal DataSource for SfPicker control which will holds the collection of Day ,Month and Year
        /// </summary>
        /// <value>The date.</value>
        public ObservableCollection<object> Date { get; set; }

        //Day is the collection of day numbers
        internal ObservableCollection<object> Day { get; set; }

        //Month is the collection of Month Names
        internal ObservableCollection<object> Month { get; set; }

        //Year is the collection of Years from 1990 to 2042
        internal ObservableCollection<object> Year { get; set; }

        //Hour is the collection of Hours in Railway time format
        internal ObservableCollection<object> Hour { get; set; }

        //Minute is the collection of Minutes from 00 to 59
        internal ObservableCollection<object> Minute { get; set; }


        /// <summary>
        /// Headers api is holds the column name for every column in date picker
        /// </summary>
        /// <value>The Headers.</value>
        public ObservableCollection<string> Headers { get; set; }

        #endregion

        public DateTimePicker()
        {
            Months = new Dictionary<string, string>();

            Date = new ObservableCollection<object>();
            Day = new ObservableCollection<object>();
            Month = new ObservableCollection<object>();
            Year = new ObservableCollection<object>();
            Hour = new ObservableCollection<object>();
            Minute = new ObservableCollection<object>();
            Headers = new ObservableCollection<string>
            {
                "Year",
                "Month",
                "Day",
                "Hour",
                "Minute"
            };
            HeaderText = "Date Time Picker";
            PopulateDateCollection();
            this.ItemsSource = Date;
            this.ColumnHeaderText = Headers;
            this.SelectionChanged += CustomDatePicker_SelectionChanged;
            ShowFooter = true;
            ShowHeader = true;
            ShowColumnHeader = true;
        }

        private void CustomDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDays(Date, e);
        }

        //Updatedays method is used to alter the Date collection as per selection change in Month column(if feb is Selected day collection has value from 1 to 28)

        public void UpdateDays(ObservableCollection<object> Date, SelectionChangedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                if (Date.Count == 5)
                {
                    bool isupdate = false;
                    if (e.OldValue != null && e.NewValue != null && (e.OldValue is ObservableCollection<object>) && (e.OldValue as ObservableCollection<object>).Count > 0)
                    {
                        if (!object.Equals((e.OldValue as IList)[1], (e.NewValue as IList)[1]))
                        {
                            isupdate = true;
                        }

                        if (!object.Equals((e.OldValue as IList)[0], (e.NewValue as IList)[0]))
                        {
                            isupdate = true;
                        }
                    }

                    if (isupdate)
                    {
                        ObservableCollection<object> days = new ObservableCollection<object>();
                        int month = DateTime.ParseExact(Months[(e.NewValue as IList)[1].ToString()], "MMMM", CultureInfo.CurrentCulture).Month;
                        int year = int.Parse((e.NewValue as IList)[0].ToString());
                        for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                        {
                            if (j < 10)
                            {
                                days.Add("0" + j);
                            }
                            else
                                days.Add(j.ToString());
                        }

                        ObservableCollection<object> oldvalue = new ObservableCollection<object>();

                        foreach (var item in e.NewValue as IList)
                        {
                            oldvalue.Add(item);
                        }

                        if (days.Count > 0)
                        {
                            Date.RemoveAt(2);
                            Date.Insert(2, days);
                        }

                        if ((Date[2] as IList).Contains(oldvalue[2]))
                        {
                            this.SelectedItem = oldvalue;
                        }
                        else
                        {
                            oldvalue[2] = (Date[2] as IList)[(Date[2] as IList).Count - 1];
                            this.SelectedItem = oldvalue;
                        }
                    }
                }
            });
        }


        private void PopulateDateCollection()
        {
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                if (i < 10)
                {
                    Day.Add("0" + i);
                }
                else
                    Day.Add(i.ToString());
            }

            //populate year
            for (int i = DateTime.Now.Year; i < 2050; i++)
            {
                Year.Add(i.ToString());
            }


            //populate months

            for (int i = 1; i < 13; i++)
            {
                if (!Months.ContainsKey(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3)))
                    Months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));
            }
            Date.Add(Year);
            Date.Add(Month);
            Date.Add(Day);

            for (int i = 1; i <= 24; i++)
            {
                if (i < 10)
                {
                    Hour.Add("0" + i.ToString());
                }
                else
                    Hour.Add(i.ToString());
            }
            for (int j = 0; j < 60; j++)
            {
                if (j < 10)
                {
                    Minute.Add("0" + j);
                }
                else
                    Minute.Add(j.ToString());
            }

            Date.Add(Hour);
            Date.Add(Minute);
        }
    }
}
