'use strict';

SocioboardApp.controller('NotificationAllController', function ($rootScope, $scope, $http, $timeout, apiDomain, domain) {
    //alert('helo');
    $scope.$on('$viewContentLoaded', function () {

        notification_all();
        //All notification
        var startNotify = 0;
        $scope.notifycount = startNotify;
        $scope.LoadAllNotifications = function () {
            
            $http.get(apiDomain + '/api/Notifications/FindAllNotifications?userId=' + $rootScope.user.Id + '&skip=' + startNotify + '&count=' + 10)
                              .then(function (response) {
                                  $scope.lstnotifications = response.data;
                                  $scope.notifycount = $scope.notifycount + 10;
                                  $http.get(apiDomain + '/api/Notifications/UpdateNotifications?userId=' + $rootScope.user.Id)
                                  .then(function (response) {
                                      $scope.updated = response.data;
                                  })
                              }, function (reason) {
                                  $scope.error = reason.data;
                              });
            // end codes to load  recent Feeds

        }
        $scope.LoadAllNotifications();

        $scope.loadmore = function () {

            

            $("#load_more_toggle").addClass("hide");
            $http.get(apiDomain + '/api/Notifications/FindAllNotifications?userId=' + $rootScope.user.Id + '&skip=' + $scope.notifycount + '&count=' + 10)
                              .then(function (response) {
                                  $scope.loadlstnotifications = response.data;
                                  $scope.notifycount = $scope.notifycount + 10;
                                  $("#load_more_toggle").removeClass("hide");
                                  $http.get(apiDomain + '/api/Notifications/UpdateNotifications?userId=' + $rootScope.user.Id)
                                  .then(function (response) {
                                      $scope.updated = response.data;
                                  })
                              }, function (reason) {
                                  $scope.error = reason.data;
                              });
            // end codes to load  recent Feeds

        }
    });
});