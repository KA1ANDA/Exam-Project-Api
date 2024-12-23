using ExamProjectApi.Models;
using Oracle.ManagedDataAccess.Client;
using System.Numerics;

namespace ExamProjectApi.Packages
{

    public interface IPKG_BOOKS
    {
        public void add_book(Book book);
        public List<Book> get_books();
        public void delete_book(Book book);
        public void update_book(Book book);
    }
    public class PKG_BOOKS:PKG_BASE , IPKG_BOOKS
    {
        IConfiguration configuration;

        public PKG_BOOKS(IConfiguration configuration) : base(configuration) { }

        public void add_book(Book book)
        {

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_books.add_book";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_name", OracleDbType.Varchar2).Value = book.Name;
            cmd.Parameters.Add("v_author", OracleDbType.Varchar2).Value = book.Author;
            cmd.Parameters.Add("v_pice", OracleDbType.Int32).Value = book.Price;
            cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = book.Quantity;

            cmd.ExecuteNonQuery();

            conn.Close();
        }


        public List<Book> get_books()
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_books.get_books";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_result", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();
            List<Book> books = new List<Book>();

            while (reader.Read())
            {
                Book book = new Book();
                book.Id = int.Parse(reader["id"].ToString());
                book.Name = reader["name"].ToString();
                book.Author = reader["author"].ToString();
                book.Price = int.Parse(reader["price"].ToString());
                book.Quantity = int.Parse(reader["quantity"].ToString());

                books.Add(book);
            }
            reader.Close();
            conn.Close();
            return books;

        }

        public void delete_book(Book book)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_books.delete_book";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_id", OracleDbType.Int32).Value = book.Id;


            cmd.ExecuteNonQuery();

            conn.Close();
        }



        public void update_book(Book book)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_books.update_book";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_id", OracleDbType.Int32).Value = book.Id;
            cmd.Parameters.Add("v_name", OracleDbType.Varchar2).Value = book.Name;
            cmd.Parameters.Add("v_author", OracleDbType.Varchar2).Value = book.Author;
            cmd.Parameters.Add("v_pice", OracleDbType.Int32).Value = book.Price;


            cmd.ExecuteNonQuery();

            conn.Close();
        }


    }
}
