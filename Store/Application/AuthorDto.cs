namespace Store.Application
{
    public class AuthorDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }
       
        public string BookAuthorGuid { get; set; }
    }
}
