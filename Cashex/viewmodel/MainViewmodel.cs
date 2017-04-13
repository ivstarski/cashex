// ----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Gazprom Space Systems">
// Copyright statement. All right reserved 
// Developer:   Ivan Starski
// Date: 13/04/2017 9:43
// </copyright>
// ----------------------------------------------------------------------

namespace Cashex.viewmodel
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using model;
    using support;

    public class MainViewModel : DependencyObject
    {
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof(string), typeof(MainViewModel));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty VolumeProperty = DependencyProperty.Register(
            "Volume", typeof(string), typeof(MainViewModel));

        public string Volume
        {
            get { return (string)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        public static readonly DependencyProperty SummaryProperty = DependencyProperty.Register(
            "Summary", typeof(decimal), typeof(MainViewModel));

        public decimal Summary
        {
            get { return (decimal)GetValue(SummaryProperty); }
            set { SetValue(SummaryProperty, value); }
        }

        public ExpenseCollection Expenses { get; set; } = new ExpenseCollection();
        public ICommand AddCommand { get; set; }
        public ICommand DelCommand { get; set; }

        public MainViewModel()
        {
            Expenses.CollectionChanged += (sender, args) => { Summary = Expenses.Sum(model => model.Volume); };
            AddCommand = new ActionCommand(() => Expenses.Add(Description, decimal.Parse(Volume)), CanAddExecute);
            DelCommand = new ActionCommand(o => Expenses.Remove(o as ExpenseModel));
            Expenses.Deserialize();
        }

        private bool CanAddExecute() =>
            Expenses.FirstOrDefault(model => string.Equals(Description, model.Description)) == null
            && !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(Volume);
    }
}