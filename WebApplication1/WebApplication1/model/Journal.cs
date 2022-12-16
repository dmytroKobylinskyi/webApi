namespace WebApplication1.model
{
    public class Journal
    {
        public int Id { get; set; }
        public string StudentLastName { get; set; }
        public string StudentName { get; set; }
        public List<double> AssessmentForLabs { get; set; }
        public double ExamAssessment { get; set; }
    }
}
