using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using HappyTime.Annotations;
using MahApps.Metro.Controls;
using NodaTime;

namespace HappyTime
{
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        public Period Difference => Period.Between(
                    new LocalDate(Properties.Settings.Default.Year, Properties.Settings.Default.Month,Properties.Settings.Default.Day),
                    new LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                    PeriodUnits.Years | PeriodUnits.Months | PeriodUnits.Days);

        public DateTime StartDateTime
        {
            get
            {
                return new DateTime(Properties.Settings.Default.Year, Properties.Settings.Default.Month, Properties.Settings.Default.Day);
            }
            set
            {
                Properties.Settings.Default.Year = value.Year;
                Properties.Settings.Default.Month = value.Month;
                Properties.Settings.Default.Day = value.Day;
                this.OnPropertyChanged(nameof(this.StartDateTime));
                this.OnPropertyChanged(nameof(this.Difference));
                this.OnPropertyChanged(nameof(this.CelebrationYearly));
                this.OnPropertyChanged(nameof(this.CelebrationMonthly));
            }
        }

        public string CelebrationYearly
        {
            get
            {
                DateTime next = this.StartDateTime.AddYears(DateTime.Now.Year - this.StartDateTime.Year);
                if (next < DateTime.Now)
                {
                    next = next.AddYears(1);
                }
                int days =  (next - DateTime.Now).Days;
                if (days == 0)
                {
                    return "NOW!";
                }
                return days.ToString();
            }
        }

        public string CelebrationMonthly
        {
            get
            {
                DateTime next = this.StartDateTime.AddYears(DateTime.Now.Year - this.StartDateTime.Year).AddMonths(DateTime.Now.Month - this.StartDateTime.Month);
                while (next < DateTime.Now)
                {
                    next = next.AddMonths(1);
                }
                int days = (next - DateTime.Now).Days;
                if (days == 0)
                {
                    return "NOW!";
                }
                return days.ToString();
            }
        }

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void ChangeTabCallback(object sender, RoutedEventArgs e)
        {

            this.TabControl.SelectedIndex = int.Parse((string) ((Button) sender).Tag);
        }

        private DateTime _cancelDateTime;

        private void SettingsWindowCommandCallback(object sender, RoutedEventArgs e)
        {
            this.SettingsPopup.IsOpen = true;
            this._cancelDateTime = this.StartDateTime;
        }

        private void CancelDateTimeChangesCallback(object sender, RoutedEventArgs e)
        {
            this.StartDateTime = this._cancelDateTime;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}