

module.exports = function(grunt)
{
  'use strict';
  
  // configuration for grunt
  grunt.initConfig({

    // loads the package configuration file
    pkg: grunt.file.readJSON('package.json'),


    // concatenate files
    concat: {
      build: {
        src: [
          'Content/Scripts/Vendor/prism/prism.js',
          'Content/Scripts/piwik.js'
        ],
        dest: 'Content/Scripts/app.js'
      }
    },


    // uglifier options
    uglify: {
      options: {
        banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n'
      },
      build: {
        src: 'Content/Scripts/app.js',
        dest: 'Release/Content/app.js'
      }
    },


    // compile compass files
    compass: {
      build: {
        options: {
          sassDir: 'Content/Sass',
          cssDir: 'Content/Sass',
          imagesDir: 'Content/Images',
          fontDir: 'Content/Fonts',
          javascriptsDir: 'Content/Scripts',
          outputStyle: 'expanded',
          environment: 'development',
          require: [
            'zurb-foundation'
          ]
        }
      },
      release: {
        options: {
          sassDir: 'Content/Sass',
          cssDir: 'Release/Content',
          imagesDir: 'Content/Images',
          fontDir: 'Content/Fonts',
          javascriptsDir: 'Content/Scripts',
          outputStyle: 'compressed',
          environment: 'production',
          require: [
            'zurb-foundation'
          ]
        }
      }
    },


    // hint all the JS files
    jshint: {
      files: [],
      options: {
        globals: {
          jQuery: true,
          console: true,
          module: true,
          document: true
        }
      }
    },


    // image optimization for dist
    imagemin: {
      release: {
        options: {
          optimizationLevel: 3
        },
        files: []
      }
    },

    // watch file changes to automate tasks
    watch: {
      js: {
        files: [ '<%= concat.build.src %>' ],
        tasks: [ 'concat' ]
      },
      sass: {
        files: [ '<%= compass.build.options.sassDir %>/**/*.scss' ],
        tasks: [ 'compass:build' ]
      }
    }
  });


  // load tasks
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-compass');
  grunt.loadNpmTasks('grunt-contrib-imagemin');
  grunt.loadNpmTasks('grunt-contrib-handlebars');
  grunt.loadNpmTasks('grunt-contrib-copy');


  // build task, which generates the development JS
  grunt.registerTask('build', [ 'concat', 'compass:build' ]);

  // distribution task
  grunt.registerTask('release', [ 'concat', 'uglify', 'compass:release' ]);

};
