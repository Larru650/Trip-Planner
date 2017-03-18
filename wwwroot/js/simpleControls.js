/// <reference path="../views/waitcursor.html" />
//simplecontrols.js
(function () {
    "use strict";
    
    angular.module("simpleControls", [])
        .directive("waitCursor", waitCursor);

    function waitCursor() {
        return{
            scope: {
                show:"=displayWhen"
            },
            restrict: "E", //we will restrict to be only the element style
            templateUrl:"../views/waitCursor.html"
        };
    }
    })();