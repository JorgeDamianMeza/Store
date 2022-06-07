namespace Store.Model
{
    public class BookAuthor
    {
        public int BookAuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public ICollection<AcademicDegree> AcademicDegreeList { get; set; }

        public string BookAuthorGuid { get; set; }

    }
}
