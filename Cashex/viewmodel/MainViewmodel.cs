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
    using mvvm.x86;
    using support;
    using view;

    public class MainViewModel : DependencyObject
    {
        public static readonly DependencyProperty BalanceProperty = DependencyProperty.Register(
            "Balance", typeof( decimal ), typeof( MainViewModel ), new PropertyMetadata( default( decimal ) ) );

        public decimal Balance
        {
            get { return (decimal)GetValue( BalanceProperty ); }
            set { SetValue( BalanceProperty, value ); }
        }

        public static readonly DependencyProperty TotalProperty = DependencyProperty.Register(
            "Total", typeof( string ), typeof( MainViewModel ), new PropertyMetadata( "1000000" ) );

        public string Total
        {
            get { return (string)GetValue( TotalProperty ); }
            set { SetValue( TotalProperty, value ); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof( string ), typeof( MainViewModel ) );

        public string Description
        {
            get { return (string)GetValue( DescriptionProperty ); }
            set { SetValue( DescriptionProperty, value ); }
        }

        public static readonly DependencyProperty VolumeProperty = DependencyProperty.Register(
            "Volume", typeof( string ), typeof( MainViewModel ) );

        public string Volume
        {
            get { return (string)GetValue( VolumeProperty ); }
            set { SetValue( VolumeProperty, value ); }
        }

        public static readonly DependencyProperty SummaryProperty = DependencyProperty.Register(
            "Summary", typeof( decimal ), typeof( MainViewModel ) );

        public decimal Summary
        {
            get { return (decimal)GetValue( SummaryProperty ); }
            set { SetValue( SummaryProperty, value ); }
        }

        public ExpenseCollection Expenses { get; set; } = new ExpenseCollection();
        public ICommand AddCommand { get; set; }
        public ICommand DelCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public MainViewModel( )
        {
            Expenses.CollectionChanged += ( sender, args ) => { CalculateSummary(); };
            AddCommand = new ActionCommand( ( ) => Expenses.Add( Description, decimal.Parse( Volume.Trim() ) ), CanAddExecute );
            DelCommand = new ActionCommand( Remove );
            EditCommand = new ActionCommand( Edit );
            Expenses.Deserialize();
        }

        private void CalculateSummary( )
        {
            Summary = Expenses.Sum( model => model.Volume );
            Balance = decimal.Parse( Total ) - Summary;
        }

        private bool CanAddExecute( ) =>
            Expenses.FirstOrDefault( model => string.Equals( Description, model.Description ) ) == null
            && !string.IsNullOrEmpty( Description ) && !string.IsNullOrEmpty( Volume );

        private void Remove( object o )
        {
            var model = o as ExpenseModel;
            if ( model != null )
                if ( MessageBox.Show( $"Действительно желаете удалить\n{o}?",
                    "Удаление расхода", MessageBoxButton.YesNo,
                    MessageBoxImage.Question ) == MessageBoxResult.Yes )
                    Expenses.Remove( (ExpenseModel)o );
        }

        private void Edit( object o )
        {
            if ( new EditWindow( o as ExpenseModel ).ShowDialog() == true )
            {
                Expenses.Serialize();
                CalculateSummary();
            }
        }
    }
}