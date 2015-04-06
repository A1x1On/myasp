(function () {
   



    angular.module('MyApp', ['ngRoute']).controller('HomeController', function ($scope, LoginService) {

        $scope.Message = "Hello";
        $scope.Elem = null;
        var val = "";
    


        LoginService.Getlogin().then(function (d) {
            $scope.Elem = d.data;  // хватаем таблицу
        }, function () { alert('Failed'); });

        $scope.myLogin = function (an_login) { // вызываемая функция при вводе
            $scope.subType = "type='button'";
            for (var e in $scope.Elem) {
                val = $scope.Elem[e];
                if (val.login == an_login) { $scope.todos2 = an_login + " Уже существует"; Islogin = 1; break; }
                else { $scope.todos2 = an_login + " Свободный"; Islogin = 0; }
            }
        };
        $scope.myMail = function (an_mail) { // вызываемая функция при вводе
            for (var e in $scope.Elem) {
                val = $scope.Elem[e];

                if (val.email == an_mail) { $scope.todos3 = an_mail + " Уже существует"; IsMail = 1; break; }
                else { $scope.todos3 = an_mail + " Свободный"; IsMail = 0; }
            }
        };

        // Проверка на правильность login и email
        $scope.regValidJs = function () {
  
           // setTimeout(function () {

                alert("2 сек");
              
           
                
              
           // }, 2000);
        };


    }).factory("LoginService", function ($http) {

        var fac = {};
        fac.Getlogin = function () {

            return $http.get('/_Public/Data/Getlogin/')
        }
        return fac;

    });





})();