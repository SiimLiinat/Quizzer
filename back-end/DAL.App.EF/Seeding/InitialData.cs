namespace DAL.App.EF.Seeding
{
    public class InitialData
    {
        public static readonly string[] Roles =
        {
            "Admin"
        };

        public static readonly (string email, string name, string password, string[] roles)[] Users =
        {
            ("siimliinat@gmail.com", "siim@liinat.com", "Admin1234_", new []{"admin"}),
            ("s@s.s", "NotSiim", "User1234_", System.Array.Empty<string>())
        };
    }
}