/// <binding AfterBuild='minify' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'); //we get gulp as an object, which obviously requires loading it
var uglify = require("gulp-uglify");
var ngAnnotate = require("gulp-ng-annotate"); 
 
gulp.task('minify', function () {  //this task will be called from the command line using gulp
    return gulp.src("wwwroot/js/*.js") //we select all the files we want to minify
                .pipe(ngAnnotate())  //we use ng annotate to be able to minify angular, otherwise the injections are being minified and will not work
                .pipe(uglify())        //we minified using our gulp-uglify tool
                .pipe(gulp.dest("wwwroot/lib/_app")); //we save these minified files into the newly created app folder
});