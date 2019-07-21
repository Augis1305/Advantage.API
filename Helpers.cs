using System;
using System.Collections.Generic;

namespace Advantage.API{
    public class Helpers{

        private static Random _rand = new Random();

         // New instance of random and we are going to return an item of random item of items we are going to pass
        private static string GetRandom(IList<string> items){
            return items[_rand.Next(items.Count)];
        }
        internal static string MakeUniqueCustomerName(List<string> names){
            var maxNames = bizPrefix.Count * bizSuffix.Count;

            if (names.Count >= maxNames){
                throw new System.InvalidOperationException("Maximum number of unique names exceeded");
            }
            
            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bizSuffix);

            var bizName = prefix + suffix;

            if (names.Contains(bizName)){
                MakeUniqueCustomerName(names);
            }
            return bizName;

        }

        private static readonly List<string> bizPrefix = new List<string>(){
            "ABC",
            "XYZ",
            "Acme",
            "MainSt",
            "Ready",
            "Magic",
            "Fluent",
            "Peak",
            "Forward",
            "Enterprise",
            "Sales"
        };

        private static readonly List<string> bizSuffix = new List<string>(){
            "Co",
            "Corp",
            "Holdings",
            "Corporation",
            "Movers",
            "Cleaners",
            "Bakery",
            "Apparel",
            "Rentals",
            "Storage",
            "Transit",
            "Logistics"
        };

        internal static string MakeCustomerEmail(string customerName){
            return $"contact@{customerName.ToLower()}.com";
        }

        internal static string GetRandomState(){
            return GetRandom(usStates);
        }
        private static readonly List<string> usStates = new List<string> (){
            "AK", "AL","AZ",  "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
        };

        internal static decimal GetRandomOrderTotal(){
            return _rand.Next(100, 5000);
        }

        internal static DateTime GetRandomOrderPlaced(){
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start; 
            // (hours, minutes, second), getting a random value of minutes where min is 0 and max total maximum minutes in our possibleSpan which is time Span from now to 90 days back
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            // Returning that day and random number of possible minutes
            return start + newSpan; 
        }

        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced){
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced; 

            if (timePassed < minLeadTime){
                return null;
            }

            return orderPlaced.AddDays(_rand.Next(7, 14));
        }
    }
}