using System;
using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Controllers{

    [Route("api/[controller]")]
    public class OrderController : Controller{
        private readonly ApiContext _ctx;

        public OrderController(ApiContext ctx){
            _ctx = ctx;
        }
        
        // Get request api/order/pageNumber/pageSize.
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize){
            var data = _ctx.Orders.Include(o => o.Customer).OrderByDescending(c => c.Placed);

            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new{
                Page = page,
                totalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("ByState")]
        public IActionResult ByState(){
            // We are include customer properties on the orders. 
            var orders = _ctx.Orders.Include(o => o.Customer).ToList();
        
            // We are taking list orders and Grouping it by selected property in this case by State. Calling ToList to have this list in a memory, and we are taking only two properties from the list(State and total number of orders).
            // And we order the total result by Total. So we have a list of states and decending number of orders for each state. 
            var groupedResults = orders.GroupBy(o => o.Customer.State)
                .ToList()
                .Select(grp => new {
                    State = grp.Key,
                    Total = grp.Sum(x => x.OrderTotal)
                }).OrderByDescending(res => res.Total).ToList();

            return Ok(groupedResults);
        }
        
        [HttpGet("ByCustomer/{n}")]
        public IActionResult ByCustomer(int n){
            // We are include customer properties on the orders. 
            var orders = _ctx.Orders.Include(o => o.Customer).ToList();

            var groupedResults = orders.GroupBy(o => o.Customer.Id)
                .ToList()
                .Select(grp => new {
                    Name = _ctx.Customers.Find(grp.Key).Name,
                    Total = grp.Sum(x => x.OrderTotal)
                }).OrderByDescending(res => res.Total).Take(n).ToList();

            return Ok(groupedResults);
        }

        [HttpGet("GetOrder/{id}", Name="GetOrder")]
        public IActionResult GetOrder(int id){

            var order = _ctx.Orders.Include(o => o.Customer)
            .First(o => o.Id == id);
             
            return Ok(order);
        }
    }
}