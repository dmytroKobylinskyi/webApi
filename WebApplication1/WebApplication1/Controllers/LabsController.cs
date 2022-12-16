using Microsoft.AspNetCore.Mvc;
using WebApplication1.model;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LabsController : ControllerBase
    {
        List<Labs> labs;

        public LabsController()
        {
            labs = new List<Labs>();
            using (JournalContext db = new JournalContext())
            {
                foreach (Labs lab in db.Labs)
                {
                    labs.Add(lab);
                }
            }
        }
        [HttpGet(Name = "GetLabs")]
        public IEnumerable<Labs> Get() => labs;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var lab = labs.SingleOrDefault(g => g.Id == id);

            if (lab == null)
            {
                return NotFound();
            }

            return Ok(lab);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lab = labs.SingleOrDefault(g => g.Id == id);
            if (lab != null)
            {
                labs.Remove(lab);
                using (JournalContext db = new JournalContext())
                {
                    lab = db.Labs.SingleOrDefault(g => g.Id == id);
                    db.Labs.Remove(lab);
                    db.SaveChanges();
                }
            }
            return Ok();
        }

        private int NextLabId =>
            (int)(labs.Count == 0 ? 1 : labs.Max(x => x.Id) + 1);

        [HttpGet("GetNextLabId")]
        public int GetNextCustomerId()
        {
            return NextLabId;
        }

        [HttpPost]
        public IActionResult Post(Labs lab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            lab.Id = NextLabId;
            labs.Add(lab);
            using (JournalContext db = new JournalContext())
            {
                db.Labs.Add(lab);
                db.SaveChanges();
            }
            return CreatedAtAction(nameof(Get), new { id = lab.Id }, lab);
        }

        [HttpPost("AddLab")]
        public IActionResult PostBody([FromBody] Labs lab) =>
                Post(lab);

        [HttpPut]
        public IActionResult Put(Labs lab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storedLab = labs.SingleOrDefault(g => g.Id == lab.Id);
            if (storedLab == null)
                return NotFound();
            storedLab.Name = lab.Name;
            storedLab.Priority = lab.Priority;
            storedLab.Status = lab.Status;
            storedLab.Text = lab.Text;
            storedLab.Time = lab.Time;

            using (JournalContext db = new JournalContext())
            {
                var labDel = db.Labs.SingleOrDefault(g => g.Id == lab.Id);
                db.Labs.Remove(labDel);
                db.Labs.Add(storedLab);
                db.SaveChanges();
            }
            return Ok(storedLab);
        }

        [HttpPut("UpdateLab")]
        public IActionResult PutBody([FromBody] Labs lab) =>
                Put(lab);


    }
}
