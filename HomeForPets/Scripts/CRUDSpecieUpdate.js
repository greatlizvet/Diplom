$(document).ready(function () {
    var categoryList = $('#Form_CategoryID');
    var specieList = $('#Form_SpecieID');

    categoryList.change(function (e) {
        $.ajax({
            url: "/Form/GetSpecies/" + categoryList.val(),
            method: "GET",
            success: function (data) {
                specieList.children().remove();
                speciesData = data.Species;
                for (specie in speciesData) {
                    specieList.append('<option value=' + speciesData[specie].Value + '>' + speciesData[specie].Text + '</option>');
                }
            }
        });
    });
});