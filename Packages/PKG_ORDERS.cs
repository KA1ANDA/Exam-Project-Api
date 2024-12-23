using ExamProjectApi.Models;
using Oracle.ManagedDataAccess.Client;

namespace ExamProjectApi.Packages
{

    public interface IPKG_ORDERS
    {
        public void create_order(Order order);
        public void add_book_to_order(OrderDetails order);
        public List<CartOrder> get_user_cart_by_customer();
        public void add_to_cart(CartOrder order);
    }


    public class PKG_ORDERS : PKG_BASE, IPKG_ORDERS
    {
        IConfiguration configuration;

        public PKG_ORDERS(IConfiguration configuration) : base(configuration) { }

        public void create_order(Order order)
        {

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_book_order.create_order";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_customer_name", OracleDbType.Varchar2).Value = order.CustomerName;
            cmd.Parameters.Add("v_total_amount", OracleDbType.Int32).Value = order.TotalAmount;
           

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void add_book_to_order(OrderDetails order)
        {

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_book_order.add_book_to_order";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_order_id", OracleDbType.Int32).Value = order.OrderId;
            cmd.Parameters.Add("v_book_id", OracleDbType.Int32).Value = order.BookId;
            cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = order.Quantity;
            cmd.Parameters.Add("v_total_price", OracleDbType.Int32).Value = order.TotalPrice;




            cmd.ExecuteNonQuery();

            conn.Close();
        }


        public List<CartOrder> get_user_cart_by_customer()
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_book_order.get_user_cart_by_customer";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_result", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();
            List<CartOrder> orderDetails = new List<CartOrder>();

            while (reader.Read())
            {
                CartOrder orderDetail = new CartOrder();
                orderDetail.Id = int.Parse(reader["id"].ToString());
                orderDetail.CustomerName = reader["customername"].ToString();
                orderDetail.BookId = int.Parse(reader["bookid"].ToString());
                orderDetail.Quantity = int.Parse(reader["quantity"].ToString());
                orderDetail.TotalPrice = int.Parse(reader["totalprice"].ToString());

                orderDetails.Add(orderDetail);
            }
            reader.Close();
            conn.Close();
            return orderDetails;

        }

        public void add_to_cart(CartOrder order)
        {

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_book_order.add_to_cart";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_customer_name", OracleDbType.Varchar2).Value = order.CustomerName;
            cmd.Parameters.Add("v_book_id", OracleDbType.Int32).Value = order.BookId;
            cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = order.Quantity;
            cmd.Parameters.Add("v_total_price", OracleDbType.Int32).Value = order.TotalPrice;




            cmd.ExecuteNonQuery();

            conn.Close();
        }

    }
}
