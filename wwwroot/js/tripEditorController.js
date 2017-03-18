//tripEditorController.js

(function () {

    "use strict";


    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {

        var vm = this;

        vm.tripName = $routeParams.tripName; //will assign to the property the parmeter that the app finds in the route. //we use pass data from the URL using angular and we pull it back out using routeParams parameter
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};
    

    var url = "/api/trips/" + vm.tripName + "/stops";

    $http.get(url)
      .then(function (response) {
          // success. we will use this data from the server to copy the data to the vm
          angular.copy(response.data, vm.stops);
          _showMap(vm.stops);
          vm.newStop = {};
        
      }, function (err) {
          // failure
          vm.errorMessage = "Failed to load stops";
      })
      .finally(function () {
          vm.isBusy = false;
      });

    vm.addStop = function () {

        vm.isBusy = true;

        $http.post(url, vm.newStop)//call the post based on the url and the newStop
                 .then(function (response) {
                     vm.stops.push(response.data);
                     _showMap(vm.stops);
                     vm.newStop = {};
                  }, function (err) {
                      vm.errorMessage = "Failed to add new stops";
                 }).finally(function () {
                     vm.isBusy = false;
                 });
                  };
                  }

    function _showMap(stops) {               //the underscore is commonly accepted for defining a function that will only be called within this context(controller), private function
        if (stops && stops.length > 0) {

            var mapStops = _.map(stops, function(item){  //as the travelmap library uses another structure, we need to adapt our stop model to that other structure
                return{                                  //therefore we change the property names to the below.

                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };
            });

            //Show map
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1, //highlight the current stop
                intialZoom: 3

            });
        }//we take the stops and we draw them on the map

    }


})();