(function () {
    function SortFilterFormData() {
        this.categoryId = '';
        this.specieId = '';
        this.search = '';
        this.sort = 'desc';
    }

    SortFilterFormData.prototype.init = function () {
        var query = document.location.search.substr(1).split('&');
        this.update(query);
    }

    SortFilterFormData.prototype.getSerializeQuery = function () {
        var query = '';
        query = 'CategoryID=' + this.categoryId
            + '&SpecieID=' + this.specieId
            + '&Search=' + this.search
            + '&Sort=' + this.sort;
        return query;
    }

    SortFilterFormData.prototype.updatePropsFromSerializeQuery = function (query) {
        var queryArray = query.split('&');
        this.update(queryArray);
    }

    SortFilterFormData.prototype.update = function (queryArray) {
        for (paramKey in queryArray) {
            paramPair = queryArray[paramKey].split('=');

            if (paramPair[0] === 'CategoryID') {
                this.categoryId = paramPair[1];
            }
            if (paramPair[0] === 'SpecieID') {
                this.specieId = paramPair[1];
            }
            if (paramPair[0] === 'Search') {
                this.search = paramPair[1];
            }
            if (paramPair[0] === 'Sort') {
                this.sort = paramPair[1];
            }
        }
    }

    var sortFilterFormData = new SortFilterFormData();
    sortFilterFormData.init();
    window.sortFilterFormData = sortFilterFormData;
})();
