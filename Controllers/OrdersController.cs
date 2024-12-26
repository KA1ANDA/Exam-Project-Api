using ExamProjectApi.Models;
using ExamProjectApi.Packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamProjectApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IPKG_ORDERS pkg_orders;

        public OrdersController(IPKG_ORDERS pkg_orders)
        {
            this.pkg_orders = pkg_orders;
        }


        [HttpPost]
        public IActionResult create_order(Order order)
        {

            int orderId = this.pkg_orders.create_order(order);

            return Ok(orderId);
        }



        [HttpPost]
        public IActionResult add_book_order_details(OrderDetail order)
        {

            this.pkg_orders.add_book_order_details(order);

            return Ok();
        }


        [HttpGet]

        public IActionResult get_orders()
        {
            List<Order> orders = new List<Order>();
            orders = this.pkg_orders.get_orders();

            return Ok(orders);
        }


        [HttpPost]

        public IActionResult get_order_details(Order order)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            orderDetails = this.pkg_orders.get_order_details(order);

            return Ok(orderDetails);
        }



        


    }
}
