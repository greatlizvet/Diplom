$(document).ready(function () {
    var categoryList = $('#CategoryID');
    var speciesList = $('#SpecieID');
    categoryList.change(function (e) {
        $.ajax({
            url: "/Home/GetSpecies/" + categoryList.val(),
            method: "GET",
            success: function(data) {
                speciesList.children().remove();
                speciesData = data.Species;
                for (specie in speciesData) {
                    speciesList.append('<option value="' + speciesData[specie].Value + '">' + speciesData[specie].Text + '</option>');
                }
            }
        });
    });
});

