namespace Demo.Api.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Application.Commands;
    using Application.Queries;

    using Microsoft.AspNetCore.Mvc;

    using Zoo.Domain.Common;

    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(
            [FromServices] IGetRestrainedAnimalsQuery<AnimalRestrained> getRestrainedAnimalsQuery)
        {
            ActionResult result = this.StatusCode((int)HttpStatusCode.InternalServerError);
            getRestrainedAnimalsQuery.Get(
                animals => result = this.Ok(animals),
                () => result = this.NotFound());
            return result;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController<TDetails, TRestrained, TCreating> : ControllerBase
        where TDetails : AnimalDetails where TRestrained : AnimalRestrained where TCreating : AnimalCreating
    {
        [HttpGet]
        public ActionResult Get(
            [FromServices] IGetRestrainedAnimalsQuery<TRestrained> getRestrainedAnimalsQuery)
        {
            ActionResult result = this.StatusCode((int)HttpStatusCode.InternalServerError);
            getRestrainedAnimalsQuery.Get(
                animals => result = this.Ok(animals),
                () => result = this.NotFound());
            return result;
        }

        [HttpGet("{id:min(1)}")]
        public ActionResult Get(
            [FromRoute] int id,
            [FromServices] IGetAnimalDetailQuery<TDetails> getAnimalDetailsQuery)
        {
            ActionResult result = this.StatusCode((int)HttpStatusCode.InternalServerError);
            getAnimalDetailsQuery.Get(
                id,
                animal => result = this.Ok(animal),
                () => result = this.NotFound());

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post(
            [FromServices] ICreateAnimalCommand<TCreating, TDetails> createAnimalCommand,
            [FromBody] TCreating createAnimal)
        {
            ActionResult result = this.StatusCode((int)HttpStatusCode.InternalServerError);
            await createAnimalCommand.CreateAsync(
                createAnimal,
                createdAnimal => result = this.Ok(createdAnimal),
                errorAnimal => result = this.BadRequest(errorAnimal));

            return result;
        }
    }
}