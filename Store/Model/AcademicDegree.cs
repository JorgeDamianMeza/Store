namespace Store.Model
{
    public class AcademicDegree
    {
        public int AcademicDegreeId { get; set; }

        public string Name { get; set; }

        public string AcademicCenter { get; set; }

        public DateTime? GradeDate { get; set; }

        public int BookAuthorId { get; set; }

        public BookAuthor BookAuthor { get; set; }

        public string AcademicDegreeGuid { get; set; }
    }
}
