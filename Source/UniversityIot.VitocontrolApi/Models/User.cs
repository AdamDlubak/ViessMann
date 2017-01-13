namespace UniversityIot.VitocontrolApi.Models
{
    public class User
    {
        public User(string userName)
        {
            Name = userName;
        }
        public string Name { get; set; }
    }
}