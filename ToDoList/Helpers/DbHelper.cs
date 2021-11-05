using Microsoft.Data.SqlClient;

namespace ToDoList.Helpers
{
    // хэлпер просто получает коннекшн, так проще, чем создавать целый класс
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=SQL5063.site4now.net;Initial Catalog=db_a7c11b_todolistdb;User Id=db_a7c11b_todolistdb_admin;Password=qwerty009");
        }
    }
}
