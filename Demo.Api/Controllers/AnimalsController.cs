﻿namespace Demo.Api.Controllers
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
            [FromRoute] FamilyType family)
        {
            var animalsByFamilyQuery = new GetAnimalsByFamilyQuery();
            var animalsByFamily = animalsByFamilyQuery.Get(family);
            if (!animalsByFamily.Any())
            {
                return this.NotFound();
            }

            var animals = animalsByFamily.Select(animal => new { animal.Id, animal.Name });
            return this.Ok(animals);
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            return this.Get(FamilyType.All);
        }

        [HttpGet("{family:enum(FamilyType)}/{id:min(1)}")]
        public ActionResult<BearDetails> Get(int id)
        {
            var getBearDetailsQuery = new GetBearDetailsQuery();
            var bear = getBearDetailsQuery.Get(id);
            if (bear is NotFoundBearDetails)
            {
                return this.NotFound(bear);
            }

            return this.Ok(bear);
        }

        [HttpPost("{family:enum(FamilyType)}")]
        public async Task<ActionResult<BearDetails>> Post(
            [FromBody] CreateBear createBear)
        {
            var createBearCommand = new CreateBearCommand();
            var createdBear = await createBearCommand.CreateAsync(createBear);
            if (createdBear is CreateBearError createBearError)
            {
                return this.BadRequest(createBearError);
            }

            return this.Ok(createdBear);
        }
    }
}