using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuoerHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero{
                    Id =1,
                    Name="Spider Man",
                    FisrtName="Peter",
                    LastName="parker",
                    Place="New York city"
                },

                new SuperHero{
                    Id =2,
                    Name="Iron Man",
                    FisrtName="Tony",
                    LastName="Stark",
                    Place="Long Island"
                }

            };
        private readonly DataContext context;

        public SuperHeroController (DataContext context)
        {
            this.context = context;
        }



        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            context.SuperHeroes.Add(hero);
            await context.SaveChangesAsync();
            return Ok( await context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbhero = await context.SuperHeroes.FindAsync(request.Id);
            if (dbhero == null)
                return BadRequest("Hero not found");

            dbhero.Name = request.Name;
            dbhero.FisrtName = request.FisrtName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;

            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");

            context.SuperHeroes.Remove(hero);
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToListAsync());
        }

    }
}
