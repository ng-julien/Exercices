namespace Demo.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Application.Commands;
    using Application.Queries;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Zoo.Domain.BearAggregate;
    using Zoo.Domain.Common;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        [HttpGet("families/{family:enum(FamilyType)}")]
        public ActionResult<IEnumerable<object>> Get(
            [FromServices] IGetAnimalsByFamilyQuery animalsByFamilyQuery,
            [FromRoute] FamilyType family)
        {
            return this.Get(animalsByFamilyQuery, family, animal => new { animal.Id, animal.Name });
        }

        [HttpGet]
        public ActionResult Get(
            [FromServices] IGetAnimalsByFamilyQuery animalsByFamilyQuery,
            [FromServices] IGetFamiliesQuery getFamiliesQuery)
        {
            var families = getFamiliesQuery.Get();
            var animalsByFamily = animalsByFamilyQuery.Get(FamilyType.All);
            if (!animalsByFamily.Any())
            {
                return this.NotFound();
            }

            var animals = animalsByFamily.Select(animal => new { animal.Id, animal.Name, animal.Family });
            return this.Ok(new { families, animals });
        }

        [HttpGet("{id:min(1)}")]
        public ActionResult Get([FromServices] IGetAnimalDetailsById getAnimalDetailsById, int id)
        {
            var animalDetails = getAnimalDetailsById.Get(id);
            if (animalDetails is NotFoundAnimalDetails)
            {
                return this.NotFound();
            }
            
            return this.Ok(animalDetails);
        }

        [HttpGet("bears/{id:min(1)}")]
        public ActionResult<BearDetails> Get([FromServices] IGetBearDetailsQuery getBearDetailsQuery, int id)
        {
            var bear = getBearDetailsQuery.Get(id);
            if (bear is NotFoundBearDetails)
            {
                return this.NotFound(bear);
            }

            return this.Ok(bear);
        }

        [HttpPost("bears")]
        public async Task<ActionResult<BearDetails>> Post(
            [FromServices] ICreateBearCommand createBearCommand,
            [FromBody] CreateBear createBear)
        {
            var createdBear = await createBearCommand.CreateAsync(createBear);
            if (createdBear is CreateBearError createBearError)
            {
                return this.BadRequest(createBearError);
            }

            return this.Ok(createdBear);
        }

        private ActionResult<IEnumerable<object>> Get(
            IGetAnimalsByFamilyQuery animalsByFamilyQuery,
            FamilyType family,
            Func<(int Id, string Name, int Family), object> transform)
        {
            var animalsByFamily = animalsByFamilyQuery.Get(family);
            if (!animalsByFamily.Any())
            {
                return this.NotFound();
            }

            var animals = animalsByFamily.Select(transform);
            return this.Ok(animals);
        }
    }
}