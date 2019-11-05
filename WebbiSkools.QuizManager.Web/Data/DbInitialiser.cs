using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.Web.Models;

namespace WebbiSkools.QuizManager.Web.Data
{
    public class DbInitialiser
    {
        public static void Initialise(QuizManagerContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User() {Username = "ViewPermissionsUser", Password = "KsZI989Bq+ce7My2vz+9xNt/XRtx6GAWM9dwZr0cX5w=", Role = "View"},
                new User() {Username = "RestrictedPermissionsUser", Password = "Hd0MMdsBKWOwCOG6W6HqdoAdIa9Z2ydtTq6OMn9Kaw8=", Role = "Restricted"},
                new User() {Username = "EditPermissionsUser", Password = "HVu5S85JnAa/lRbzu387Hyveq3iKBt5HIrAYxa4beME=", Role = "Edit"}
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}
