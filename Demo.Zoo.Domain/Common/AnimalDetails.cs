namespace Demo.Zoo.Domain.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BearAggregate;

    using GiraffeAggregate;

    using LionAggregate;

    public partial class AnimalDetails
    {
        public AnimalDetails()
        {
        }

        protected AnimalDetails(AnimalDetails animalDetails)
        {
            this.Id = animalDetails.Id;
            this.Foods = animalDetails.Foods;
            this.Legs = animalDetails.Legs;
            this.Name = animalDetails.Name;
            this.FamilyId = animalDetails.FamilyId;
        }

        public IList<string> Foods { get; set; } = Enumerable.Empty<string>().ToList();

        public int Id { get; set; }

        public int Legs { get; set; }

        public string Name { get; set; } = string.Empty;

        public int FamilyId { private get; set; }

        public AnimalDetails ToSpecifiqueAnimal()
        {
            switch (this.FamilyId)
            {
                case (int)FamilyType.Bears:
                    return new BearDetails(this);
                case (int)FamilyType.Giraffes:
                    return new GiraffeDetails(this);
                case (int)FamilyType.Lions:
                    return new LionDetails(this);
                default:
                    return new NotFoundAnimalDetails();
            }
        }
    }
}