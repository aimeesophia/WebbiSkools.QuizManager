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
                new User() {Username = "ViewPermissionsUser", Password = "password", Role = "View"},
                new User() {Username = "RestrictedPermissionsUser", Password = "password", Role = "Restricted"},
                new User() {Username = "EditPermissionsUser", Password = "password", Role = "Edit"}
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}
