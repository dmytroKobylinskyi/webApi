using Microsoft.AspNetCore.Mvc;
using WebApplication1.model;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        List<Journal> journals;

        public JournalController()
        {
            journals = new List<Journal>();
            using (JournalContext db = new JournalContext())
            {
                foreach (Journal journal in db.Journal)
                {
                    journals.Add(journal);
                }
            }
        }
        [HttpGet(Name = "GetJournals")]
        public IEnumerable<Journal> Get() => journals;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var journal = journals.SingleOrDefault(g => g.Id == id);

            if (journal == null)
            {
                return NotFound();
            }

            return Ok(journal);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var journal = journals.SingleOrDefault(g => g.Id == id);
            if (journal != null)
            {
                journals.Remove(journal);
                using (JournalContext db = new JournalContext())
                {
                    journal = db.Journal.SingleOrDefault(g => g.Id == id);
                    db.Journal.Remove(journal);
                    db.SaveChanges();
                }
            }
            return Ok();
        }

        private int NextJournalId =>
            (int)(journals.Count == 0 ? 1 : journals.Max(x => x.Id) + 1);

        [HttpGet("GetNextJournalId")]
        public int GetNextCustomerId()
        {
            return NextJournalId;
        }

        [HttpPost]
        public IActionResult Post(Journal journal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            journal.Id = NextJournalId;
            journals.Add(journal);
            using (JournalContext db = new JournalContext())
            {
                db.Journal.Add(journal);
                db.SaveChanges();
            }
            return CreatedAtAction(nameof(Get), new { id = journal.Id }, journal);
        }

        [HttpPost("AddJournal")]
        public IActionResult PostBody([FromBody] Journal journal) =>
                Post(journal);

        [HttpPut]
        public IActionResult Put(Journal journal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storedJournal = journals.SingleOrDefault(g => g.Id == journal.Id);
            if (storedJournal == null)
                return NotFound();
            storedJournal.StudentName = journal.StudentName;
            storedJournal.StudentLastName = journal.StudentLastName;
            storedJournal.AssessmentForLabs = journal.AssessmentForLabs;
            storedJournal.ExamAssessment = journal.ExamAssessment;

            using (JournalContext db = new JournalContext())
            {
                var journalDel = db.Journal.SingleOrDefault(g => g.Id == journal.Id);
                db.Journal.Remove(journalDel);
                db.Journal.Add(storedJournal);
                db.SaveChanges();
            }
            return Ok(storedJournal);
        }

        [HttpPut("UpdateJournal")]
        public IActionResult PutBody([FromBody] Journal journal) =>
                Put(journal);


    }
}
