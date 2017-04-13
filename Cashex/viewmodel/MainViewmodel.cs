// ----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Gazprom Space Systems">
// Copyright statement. All right reserved 
// Developer:   Ivan Starski
// Date: 13/04/2017 9:43
// </copyright>
// ----------------------------------------------------------------------

namespace Cashex.viewmodel
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using model;
    using support;

    public class MainViewModel : INotifyPropertyChanged
    {
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        private string volume;
        public string Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Volume)));
            }
        }

        private decimal summary;
        public decimal Summary
        {
            get { return summary; }
            set
            {
                summary = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Summary)));
            }
        }

        public ExpenseCollection Expenses { get; set; } = new ExpenseCollection();
        public ICommand AddCommand { get; set; }
        public ICommand DelCommand { get; set; }

        public MainViewModel()
        {
            Expenses.CollectionChanged += (sender, args) => { Summary = Expenses.Sum(model => model.Volume); };
            Expenses.Deserialize();
            AddCommand = new ActionCommand(() => Expenses.Add(description, decimal.Parse(volume)), CanAddExecute);
            DelCommand = new ActionCommand(o => Expenses.Remove(o as ExpenseModel));
        }

        private bool CanAddExecute() =>
            Expenses.FirstOrDefault(model => string.Equals(description, model.Description)) == null
            && !string.IsNullOrEmpty(description) && !string.IsNullOrEmpty(volume);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}