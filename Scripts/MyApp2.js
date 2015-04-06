angular.module('MyApp').controller('MyApp2', function ($scope, ContactService) {
    $scope.mess = "MyApp2 контроллер работает";
    $scope.User = null;
    ContactService.GetLastContact().then(function (d) {
        $scope.User = d.data;
    }, function () {

        alert('Failed');
    });



}).factory('ContactService', function ($http) {

    var fac = {};
    fac.GetLastContact = function () {

        return $http.get('/_Public/Data/GetLastContact/')
    }
    return fac;

});