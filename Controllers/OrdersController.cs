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
            
            this.pkg_orders.create_order(order);
            return Ok();
        }


        [HttpPost]
        public IActionResult add_book_to_order(OrderDetails order)
        {

            this.pkg_orders.add_book_to_order(order);
            return Ok();
        }


        [HttpPost]
        public IActionResult add_to_cart(CartOrder order)
        {

            this.pkg_orders.add_to_cart(order);
            return Ok();
        }


        [HttpGet]
        public IActionResult get_user_cart_by_customer()
        {

            List<CartOrder> orders = new List<CartOrder>();
            orders = this.pkg_orders.get_user_cart_by_customer();

            return Ok(orders);
        }





        
    }
}
