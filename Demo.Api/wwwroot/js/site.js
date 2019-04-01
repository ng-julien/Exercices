// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function(window, $) {
    var animalsElm = $(".animals");
    let origin = window.origin;
    $.getJSON(origin + '/api/animals',
        function (response) {
            var animals = response.animals;
            var families = response.families;

            var mappedAnimals = animals.map(animal => {
                var family = families.find(family => family.id === animal.family).label;
                return {
                        id: animal.id,
                        name: animal.name,
                        family: family
                    };
            });

            animalsElm.loadTemplate(origin + "/animal/animal-item.html", mappedAnimals,
                {
                    success: () => {
                        $(".animals-animal > a").bind("click",
                            function(event) {
                                var detailurl = origin + '/api/animals/' + event.target.id;
                                $.getJSON(detailurl,
                                    function(response) {
                                        console.log(response);
                                    });
                            });
                    }
                });
        });
})(window, window.$);