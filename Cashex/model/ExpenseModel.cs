// ----------------------------------------------------------------------
// <copyright file="ExpenseModel.cs" company="Gazprom Space Systems">
// Copyright statement. All right reserved 
// Developer:   Ivan Starski
// Date: 13/04/2017 9:44
// </copyright>
// ----------------------------------------------------------------------

namespace Cashex.model
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ExpenseModel : INotifyPropertyChanged 
    {
        private string description;
        private decimal volume;

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public decimal Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                OnPropertyChanged();
            }
        }

        public override string ToString() => $"Расход: {Description}, сумма: {Volume} р.";
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}