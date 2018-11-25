(function () {
    //Module
    var app = angular.module('MyApp', []);
    //Controller
    app.controller('BlogCtrl', ['$scope', '$window', '$http', '$q', blogCtrl]);
    app.controller('HomeCtrl', homeCtrl);

    //Services

})();
