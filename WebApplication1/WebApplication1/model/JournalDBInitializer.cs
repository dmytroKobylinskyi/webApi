using System.Data.Entity;

namespace WebApplication1.model
{
    public class JournalDBInitializer : DropCreateDatabaseAlways<JournalContext>
    {
        protected override void Seed(JournalContext context)
        {
            IList<Labs> labs = new List<Labs>();

            labs.Add(new Labs() { Id = 1, Name = "Lab 1", Text = "dqwqwq", Time = new DateTime(2022,9,20), Priority = 9, Status = true});
            labs.Add(new Labs() { Id = 1, Name = "Lab 2", Text = "wee", Time = new DateTime(2022,12,24), Priority = 10, Status = false});
            labs.Add(new Labs() { Id = 1, Name = "Lab 3", Text = "fssgr", Time = new DateTime(2022,11,30), Priority = 8, Status = true});
            

            context.Labs.AddRange(labs);

            IList<Journal> journals = new List<Journal>();
            journals.Add(new Journal() { Id = 1, StudentLastName = "Oraw", StudentName = "Qent", AssessmentForLabs = new List<double> {5,2,1}, ExamAssessment = 40});
            journals.Add(new Journal() { Id = 1, StudentLastName = "Kellen", StudentName = "Bill", AssessmentForLabs = new List<double> {5,3}, ExamAssessment = 45});
            journals.Add(new Journal() { Id = 1, StudentLastName = "Undo", StudentName = "Sin", AssessmentForLabs = new List<double> {5,5,4}, ExamAssessment = 50});

            context.Journal.AddRange(journals);
            base.Seed(context);
        }
    }
}
