(function () {

    var app = angular.module("IransabzcncHomeViewer", []);

    var SliderController = function ($scope, $http) {

        var onComplete = function (response) {
            $scope.SliderArray = response.data;
        };

        var onError = function (reason) {
            $scope.SlideError = "Error retrieving slides data";
        };

        $http.get("/api/SlidesApi")
             .then(onComplete, onError);
    };

    var AboutController = function ($scope, $http, $sce) {

        var onComplete = function (response) {
            $scope.AboutArray = response.data;
            $scope.trustAsHtml = $sce.trustAsHtml;
        };

        var onError = function (reason) {
            $scope.AboutError = "Error retrieving about data";
        };

        $http.get("/api/AboutUsApi")
             .then(onComplete, onError);

    };

    var ServicesController = function ($scope, $http) {

        var onComplete = function (response) {
            $scope.ServicesArray = response.data;
        };

        var onError = function (reason) {
            $scope.ServicesError = "Error retrieving services data";
        };

        $http.get("/api/ServicesApi")
             .then(onComplete, onError);

    };

    var MemberController = function ($scope, $http) {

        var onComplete = function (response) {
            $scope.MembersArray = response.data;
        };

        var onError = function (reason) {
            $scope.MembersError = "Error retrieving services data";
        };

        $http.get("/api/MembersApi")
        .then(onComplete, onError);
    };

    app.controller("SliderController", ["$scope", "$http", SliderController]);
    app.controller("AboutController", ["$scope", "$http", "$sce", AboutController]);
    app.controller("ServicesController", ["$scope", "$http", ServicesController]);
    app.controller("MemberController", ["$scope", "$http", MemberController]);


}());