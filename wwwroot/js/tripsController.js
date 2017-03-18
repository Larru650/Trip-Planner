//tripsController.js
(function(){

    "use strict";
    //  Getting the existing the module
    //here the module is already created and we are referencing it to create a controller
    angular.module("app-trips")
        .controller("tripsController", tripsController); //create a controller under tripscontrollername with the below function
    
    function tripsController($http) {



        var vm = this; //more descriptive calling viewmodel than this all the time

        vm.trips = [];

        vm.newTrip = {}; //we will push the data using angular in our view to this viewmodel

        vm.errorMessage = ""; // non functional data. 

        vm.isBusy = true;

        $http.get("/api/trips") //the get will return a "promise" where we can get the results, errors or actions
                   .then(function (response) {
                       //success
                       angular.copy(response.data, vm.trips);

                   }, function (error) {
                       //failure
                       vm.errorMessage = "Failed to load data: " + error; //as it's JS the error will be shown if there is a failure
                   })
                    .finally(function () {
                     vm.isBusy = false;
                 });


        vm.addTrip = function () {
          
            vm.isBusy = true;
            vm.errorMessage = ""; //we restart the error message just in case we want to retry the post

            $http.post("/api/trips", vm.newTrip)//the post mthod takes and URI and the body (as postman). Body in this case is the newtrip 
                      .then(function (response) {
                             //success
                            vm.trips.push(response.data); //we push the data we take from the post method to our trips viewmodel
                            vm.newTrip = {}; //we clear the form if we succeed saving the new data into the server

                         }, function () {
                             //failure
                            vm.errorMessage = "Failed to save new trip";
                    }).finally(function () {
                            vm.isBusy = false; });
                     };
             }

    })();

//the controller is the responsible for the data, and we will use data binding to (ng-model , {{}}) to expose this data
//we use Cluenext sintax to add more actions to the function