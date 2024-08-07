namespace Roomzy.Models
{
    public class Agents
    {
        public Guid Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string User_Name { get; set; }
        public string User_Password { get; set; }
        public string User_Email { get; set; }
        public string User_Phone { get; set; }
        public string User_Address { get; set; }
        public string Remark { get; set; }
        public bool Deleted { get; set; } // Added for soft delete
    }
}
