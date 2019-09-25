// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function (window, $, Humanizer) {
    const animalsElm = $(".animals");
    const origin = window.origin;
    $.getJSON(origin.concat('/api/animals'),
        (animals) => {
            const mappedAnimals = animals.map(toAnimal);

            animalsElm.loadTemplate(origin + "/animal/animal-item.html",
                mappedAnimals,
                {
                    success: () => $(".animals-animal > a").bind("click", getAnimalDetails)
                });
        });

    function toAnimal(animal) {
        return {
                id: animal.id,
                name: animal.name,
                family: animal.family
            };
    }

    function getAnimalDetails({ target: { id, dataset: { family } } }) {
        const detailUrl = origin.concat('/api/', family.pluralize(), '/', id);
        $.getJSON(detailUrl, response => console.log(response));
    }
})(window, window.$, window.Humanizer);