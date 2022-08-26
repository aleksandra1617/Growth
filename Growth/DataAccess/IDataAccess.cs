
namespace DataAccess
{
    public interface IDataAccess
    {
        public bool Connect();
        public void SetDBName(string name);

        public bool CreateTable();
        public string? GetData(string tableName);

        // Functions that may be required soon.
        //public bool CreateNewDB();
        //public bool Disconnect();
    }
}
