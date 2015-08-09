var gulp = require('gulp');

var copy = require('copy-files');

gulp.task('init', function() {
    copy({
        files: {
            'angular.js': __dirname + '/node_modules/angular/angular.js'
        },
        dest: 'public/js/'
    }, function(err) {
        console.log(err);
    });
});
