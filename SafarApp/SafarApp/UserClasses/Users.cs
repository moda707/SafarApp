using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarApp.DbClasses;
using SafarApp.GenFunctions;

namespace SafarApp.UserClasses
{
    public class Users
    {
        [BsonId] public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }
        public DateTime LastActivity { get; set; }

        public Users()
        {
            
        }

        public static FuncResult AddUser(Users user)
        {
            //return DbConnection.FastAddorUpdate(user, CollectionNames.User);

            var dbConnection = new DbConnection();
            dbConnection.Connect();
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("email", user.Email, FieldType.String, CompareType.Equal)
            };
            var u = dbConnection.GetFilteredList<Users>(CollectionNames.User, filter);
            return u.Count > 0
                ? FuncResult.Unsuccessful
                : DbConnection.FastAddorUpdate(user, CollectionNames.User);
        }

        public static Users LoginUser(string email, string password)
        {
            var dbConnection = new DbConnection();
            dbConnection.Connect();
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("Email", email, FieldType.String, CompareType.Equal),
                new FieldFilter("Password", password, FieldType.String, CompareType.Equal)
            };

            var u = dbConnection.GetFilteredList<Users>(CollectionNames.User, filter);
            return u.Count > 0 ? u[0] : null;
        }

        public static Users getUserById(ObjectId userId)
        {

            return new Users();
        }
    }
}
