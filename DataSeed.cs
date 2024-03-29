using System;
using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models;
// using Advantage.API.Helpers;

namespace Advantage.API{
    public class DataSeed{
        private readonly ApiContext _ctx;

        public DataSeed(ApiContext ctx){
            _ctx = ctx;
        }

        public void SeedData(int nCustomers, int nOrders){
            if (!_ctx.Customers.Any()){
                SeedCustomers(nCustomers);
                _ctx.SaveChanges();
            }
            if (!_ctx.Orders.Any()){
                SeedOrders(nOrders);
                _ctx.SaveChanges();
            }
            if (!_ctx.Servers.Any()){
                SeedServers();
                _ctx.SaveChanges();
            }


        }
        
        // Receives a return list of Customers and adds them to database.
        private  void SeedCustomers(int n){
            List<Customer> customers = BuildCustomerList(n);
            foreach(var customer in customers){
                _ctx.Customers.Add(customer);
            }
        }

        private void SeedOrders(int n){
            List<Order> orders = BuildOrderList(n);
            foreach(var order in orders){
                _ctx.Orders.Add(order);
            }
        }
        // We are passing BuildCustomerList with integer number of nCustomers.
        private List<Customer> BuildCustomerList(int nCustomers){

            var customers = new List<Customer>();
            // We create var names where we can store the names 
            var names = new List<string>();

            for( var i = 1; i <= nCustomers; i++){
                // For var i in range of nCustomers we generate a UniqueCustomerName and add to the list of names
                var name = Helpers.MakeUniqueCustomerName(names);
                names.Add(name);

                // Finally, to the customer list we add new customer with name email and state, which are automaticly generated or picked from the list.
                customers.Add(new Customer {
                    Id = i,
                    Name = name,
                    Email = Helpers.MakeCustomerEmail(name),
                    State = Helpers.GetRandomState()
                });
            }
            
            // Return a list of customers
            return customers;
        }

        private List<Order> BuildOrderList(int nOrders){

            var orders = new List<Order>();
            var rand = new Random();
            for( var i = 1; i <= nOrders; i++){

                var randCustomerId = rand.Next(1, _ctx.Customers.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);
                var customers = _ctx.Customers.ToList(); 

                orders.Add(new Order{
                    Id = i,
                    Customer = customers.First(c => c.Id == randCustomerId),
                    OrderTotal = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed
                });
            }

            return orders;
        }

        private void SeedServers(){
            List<Server> servers = BuildServerList();

            foreach(var server in servers){
                _ctx.Servers.Add(server); 
            }
        }

        private List<Server> BuildServerList(){
            return new List<Server>(){
                new Server {
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true,

                },

                new Server {
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = false

                },

                new Server {
                    Id = 3,
                    Name = "Dev-Services",
                    IsOnline = true

                },

                new Server {
                    Id = 4,
                    Name = "QA-Web",
                    IsOnline = true,

                },

                new Server {
                    Id = 5,
                    Name = "QA-Web",
                    IsOnline = false,

                },

                new Server {
                    Id = 6,
                    Name = "QA-Web",
                    IsOnline = true,

                },
                new Server {
                    Id = 7,
                    Name = "Prob-Web",
                    IsOnline = true,

                },

                new Server {
                    Id = 8,
                    Name = "Prob-Web",
                    IsOnline = false,

                },

                new Server {
                    Id = 9,
                    Name = "Prob-Web",
                    IsOnline = true,

                },
            };
        }
    }
}