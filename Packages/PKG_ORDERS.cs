using ExamProjectApi.Models;
using Oracle.ManagedDataAccess.Client;

namespace ExamProjectApi.Packages
{

    public interface IPKG_ORDERS
    {
        public int create_order(Order order);
        public void add_book_order_details(OrderDetail order);
        public List<Order> get_orders();
        public List<OrderDetail> get_order_details(Order order);


    }


    public class PKG_ORDERS : PKG_BASE, IPKG_ORDERS
    {
        IConfiguration configuration;

        public PKG_ORDERS(IConfiguration configuration) : base(configuration) { }

        public int create_order(Order order)
        {
            int orderId = 0;

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_book_order.create_order";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_customer_name", OracleDbType.Varchar2).Value = order.UserName;
            cmd.Parameters.Add("v_total_amount", OracleDbType.Varchar2).Value = order.TotalAmount;

            OracleParameter outputOrderId = new OracleParameter("v_order_id", OracleDbType.Int32)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            cmd.Parameters.Add(outputOrderId);


            cmd.ExecuteNonQuery();

            orderId = Convert.ToInt32(outputOrderId.Value.ToString());

            conn.Close();

            return orderId;
        }



        public void add_book_order_details(OrderDetail order)
        {
            int orderId = 0;

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_book_order.add_book_order_details";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_order_id", OracleDbType.Int32).Value = order.OrderId;
            cmd.Parameters.Add("v_book_id", OracleDbType.Int32).Value = order.BookId;
            cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = order.Quantity;
            cmd.Parameters.Add("v_total_price", OracleDbType.Int32).Value = order.TotalPrice;



            cmd.ExecuteNonQuery();

           

            conn.Close();

        }



        public List<Order> get_orders()
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_book_order.get_orders";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_result", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();
            List<Order> orders = new List<Order>();

            while (reader.Read())
            {
                Order order = new Order();
                order.Id = int.Parse(reader["id"].ToString());
                order.UserName = reader["customername"].ToString();
                order.TotalAmount = int.Parse(reader["totalamount"].ToString());


                orders.Add(order);
            }
            reader.Close();
            conn.Close();
            return orders;

        }




        public List<OrderDetail> get_order_details(Order order)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_tk_book_order.get_order_details";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("v_id", OracleDbType.Int32).Value = order.Id;
            cmd.Parameters.Add("v_result", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            while (reader.Read())
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.Id = int.Parse(reader["id"].ToString());
                orderDetail.OrderId = int.Parse(reader["orderid"].ToString());
                orderDetail.BookId = int.Parse(reader["bookid"].ToString());
                orderDetail.Quantity = int.Parse(reader["quantity"].ToString());
                orderDetail.TotalPrice = int.Parse(reader["totalprice"].ToString());


                orderDetails.Add(orderDetail);
            }
            reader.Close();
            conn.Close();
            return orderDetails;

        }

    }
}
