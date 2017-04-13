// ----------------------------------------------------------------------
// <copyright file="Hash.cs" company="Gazprom Space Systems">
// Copyright statement. All right reserved 
// Developer:   Ivan Starski
// Date: 13/04/2017 10:13
// </copyright>
// ----------------------------------------------------------------------
namespace Cashex.support
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using model;

    public class ExpenseCollection : ObservableCollection<ExpenseModel>
    {
        private const string FILENAME = "data.dat";

        private void Serialize()
        {
            using( var stream = new MemoryStream() )
            {
                new DataContractSerializer(GetType()).WriteObject(stream, this.ToList());
                File.WriteAllBytes(FILENAME, stream.ToArray());
            }
        }

        public new void Add(ExpenseModel item)
        {
            base.Add(item);
            Serialize();
        }

        public void Add(string description, decimal volume)
        {
            Add(new ExpenseModel { Description = description, Volume = volume });
        }

        public new void Remove(ExpenseModel item)
        {
            base.Remove(item);
            Serialize();
        }

        public void Deserialize()
        {
            if( false == File.Exists(FILENAME) )
                return;

            using( var stream = new MemoryStream(File.ReadAllBytes(FILENAME)) )
            {
                stream.Position = 0;
                try
                {
                    var collection = (ExpenseCollection)new DataContractSerializer(typeof(ExpenseCollection)).ReadObject(stream);
                    foreach( var expenseModel in collection )
                        base.Add(expenseModel);
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}