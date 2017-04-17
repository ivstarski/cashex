namespace Cashex.view
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using model;

    public partial class EditWindow
    {
        private ICollectionView typesView;

        private readonly ExpenseModel expenseModel;
        public EditWindow(ExpenseModel expenseModel)
        {
            this.expenseModel = expenseModel;
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            expense.Text = expenseModel.Description;
            sum.Text = expenseModel.Volume.ToString();

            typesView = CollectionViewSource.GetDefaultView(Enum.GetNames(typeof(ExpenseType)));
            type.ItemsSource = typesView;
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            expenseModel.Description = expense.Text;
            expenseModel.Volume = decimal.Parse(sum.Text);
            DialogResult = true;
        }

        private void Cancel(object sender, RoutedEventArgs e) => Close();
    }
}