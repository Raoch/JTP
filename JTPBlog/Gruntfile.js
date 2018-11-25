/// <binding BeforeBuild='development' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        concat:
        {
            // all is Target
            all:
            {
                src: ['CustomScripts/*.js','CustomScripts/**/*.js'],
                dest: 'wwwroot/js/site.js'
            }
        },
        uglify:
        {
            // js is Target 
            js:
                {
                    //Source of files
                src: ['wwwroot/concat/js/*.js'],
                    //Destination of files
                    dest: 'wwwroot/production/site.js'
                }
        },
        cssmin:
        {
            // css is Target 
            css:
            {
                //Source of files
                src: ['wwwroot/css/site.css'],
                //Destination of files
                dest: 'wwwroot/css/site.min.css'
            }
        },
        // Sass
        sass: {
            options: {
                sourceMap: true, // Create source map
                outputStyle: 'compressed' // Minify output
            },
            dist: {
                files: [
                    {
                        expand: true, // Recursive
                        src: ["Styles/*.scss"], // Source files
                        dest: "wwwroot/css", // Destination
                        ext: ".css" // File extension
                    }
                ]
            }
        },
        tinyimg:
        {
            // minifyimage is Target
            minifyimage:
            {
                // Source of files
                src: ['wwwroot/images/**/*.{png,jpg,gif,svg}'],
                // Destination of files
                dest: 'wwwroot/Compressimages/',
                expand: true,
                flatten: true
            }
        }
    });
    grunt.loadNpmTasks("grunt-contrib-concat");
    grunt.loadNpmTasks("grunt-contrib-uglify-es");
    grunt.loadNpmTasks("grunt-contrib-cssmin");
    grunt.loadNpmTasks("grunt-tinyimg");
    grunt.loadNpmTasks('grunt-sass');


    grunt.registerTask("development", ['concat'/*, 'uglify'*/, 'cssmin', 'sass']);

    grunt.registerTask("production", ['concat', /*'uglify',*/ 'cssmin'/*, 'tinyimg'*/]);
};