using Microsoft.AspNetCore.Mvc;
using elm_10_web_api_githubra.JokeModels;

namespace elm_10_web_api_githubra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/jokes/5 >>> joke bekérése (keresés)
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            var keresettVicc = (from x in context.Jokes
                                where x.JokeSk == id
                                select x).FirstOrDefault();
            if (keresettVicc == null)
            {
                return NotFound($"Nincs #{id} azonosítóval vicc");
            }
            else
            {
                return Ok(keresettVicc);
            }
        }



        // POST api/jokes >>> joke rögzítése
        [HttpPost]
        public void Post([FromBody] Joke újVicc)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            context.Jokes.Add(újVicc);
            context.SaveChanges();
        }



        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Joke joke)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            //a külön részeken is létre kell hozni adatbázis kapcsolatokat!
            var meglevoVicc = (from x in context.Jokes
                              where x.JokeSk == id
                              select x).FirstOrDefault();
            if (meglevoVicc == null)
            {
                return NotFound($"Nincs #{id} azonosítóval vicc");
                //nem jó a return, ezért a public void helyett "public IActionResult"!
            }
            else
            {
                //frissítés
                meglevoVicc.JokeText = joke.JokeText;
                meglevoVicc.UpVotes = joke.UpVotes;
                meglevoVicc.DownVotes = joke.DownVotes;
                
                context.Jokes.Update(meglevoVicc);
                context.SaveChanges();

                return Ok(meglevoVicc);
            }
        }


        // DELETE api/jokes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            var törlendőVicc = (from x in context.Jokes
                                where x.JokeSk == id
                                select x).FirstOrDefault();
            context.Jokes.Remove(törlendőVicc);
            context.SaveChanges();
        }

    }
}
