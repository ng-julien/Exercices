namespace Demo.Api.Controllers
{
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
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
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
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public ActionResult Get(
            [FromServices] IGetRestrainedAnimalsQuery<TRestrained> getRestrainedAnimalsQuery)
        {
            ActionResult result = this.StatusCode((int)HttpStatusCode.InternalServerError);
            getRestrainedAnimalsQuery.Get(
                animals => result = this.Ok(animals),
                () => result = this.StatusCode((int)HttpStatusCode.NoContent));
            return result;
        }

        [HttpGet("{id:min(1)}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Post(
            [FromServices] ICreateAnimalCommand<TCreating, TDetails> createAnimalCommand,
            [FromBody] TCreating createAnimal)
        {
            ActionResult result = this.StatusCode((int)HttpStatusCode.InternalServerError);
            await createAnimalCommand.CreateAsync(
                createAnimal,
                createdAnimal => result = this.Created(
                                     $"api/{this.RouteData.Values["controller"]}/{createdAnimal.Id}",
                                     createdAnimal),
                errorAnimal => result = this.BadRequest(errorAnimal));

            return result;
        }
    }
}