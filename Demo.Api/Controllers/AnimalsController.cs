namespace Demo.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Application.Commands;
    using Application.Queries;

    using Infrastructure.Specifications;

    using Microsoft.AspNetCore.Mvc;

    using Zoo.Domain.BearAggregate;

    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        [HttpGet("{family:enum(FamilyType)}")]
        public ActionResult<IEnumerable<object>> Get(
            [FromServices] IGetAnimalsByFamilyQuery animalsByFamilyQuery,
            [FromRoute] FamilyType family)
        {
            var animalsByFamily = animalsByFamilyQuery.Get(family);
            var animals = animalsByFamily.Select(animal => new { animal.Id, animal.Name });
            return this.Ok(animals);
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get([FromServices] IGetAnimalsByFamilyQuery animalsByFamilyQuery)
        {
            return this.Get(animalsByFamilyQuery, FamilyType.All);
        }

        [HttpGet("{family:enum(FamilyType)}/{id:min(1)}")]
        public ActionResult<BearDetails> Get([FromServices] IGetBearDetailsQuery getBearDetailsQuery, int id)
        {
            var bear = getBearDetailsQuery.Get(id);
            return this.Ok(bear);
        }

        [HttpPost("{family:enum(FamilyType)}")]
        public async Task<ActionResult<BearDetails>> Post(
            [FromServices] ICreateBearCommand createBearCommand,
            [FromBody] CreateBear createBear)
        {
            var createdBear = await createBearCommand.CreateAsync(createBear);
            return this.Ok(createdBear);
        }
    }
}