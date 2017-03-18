//app-trips.js this is the trip view of the app controller
//we start with an immediately invoking function expression to take all of the code out of the global scope
(function () {

    "use strict";
    //Creating the module
    angular.module("app-trips", ["simpleControls", "ngRoute"])
    .config(function($routeProvider){
    
        $routeProvider.when("/",
            {
                controller: "tripsController",
                controllerAs: "vm",
                templateUrl: "/views/tripsView.html" //this will add a # in the browser URL which means that after that # is going to be client side routes.
                                                    //this will swap out the html fragmts and let the app activate or deactivate the controllers 
            });
        $routeProvider.when("/editor/:tripName",{ //we introduce a variable into the route with ":". This way we can have a route that handles a single trip {
            controller: "tripEditorController",
            controllerAs: "vm",
            templateUrl: "/views/tripEditorView.html"
        });


         $routeProvider.otherwise({redirectTo: "/"});

        


    }); //we are "bootstraping" this module into our view. We need to inject the dependencies we are going to use for this module
    //the second parameter gives us a hint that we are defining the module instead of referencing it
})();