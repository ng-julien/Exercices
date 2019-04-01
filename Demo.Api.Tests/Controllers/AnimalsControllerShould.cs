namespace Demo.Api.Tests.Controllers
{
    using Demo.Api.Controllers;
    using Demo.Application.Exceptions;
    using Demo.Application.Queries;
    using Demo.Infrastructure.Specifications;
    using Moq;
    using Xunit;
    using Shouldly;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class AnimalsControllerShould
    {
        [Fact]
        public void ReturnNotFoundWhenCallGetAndNotAnimalsFound()
        {
            var bearFamily = FamilyType.Bears;
            var animalsByFamilyQueryMock = new Mock<IGetAnimalsByFamilyQuery>();
            animalsByFamilyQueryMock
                .Setup(query => query.Get(bearFamily))
                .Returns(new NotFoundAnimals());
            var animalsController = new AnimalsController();

            var response = animalsController.Get(animalsByFamilyQueryMock.Object, FamilyType.All);

            response.ShouldNotBeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public void ReturnOkWithAnimalsWhenCallGetAndFoundThem()
        {
            var expectedAnimals = new List<(int Id, string Name, int Family)>();
            var bearFamily = FamilyType.Bears;
            var animalsByFamilyQueryMock = new Mock<IGetAnimalsByFamilyQuery>();
            animalsByFamilyQueryMock
                .Setup(query => query.Get(bearFamily))
                .Returns(expectedAnimals);
            var animalsController = new AnimalsController();

            var response = animalsController.Get(animalsByFamilyQueryMock.Object, FamilyType.All);

            var okRresponse = response.ShouldBeOfType<OkObjectResult>();
            var content = okRresponse.Value.ShouldBeOfType<List<(int Id, string Name, int Family)>>();
            content.ShouldBe(expectedAnimals);
        }
    }
}