

module.exports = function(grunt)
{

  // configuration for grunt
  grunt.initConfig({

    // loads the package configuration file
    pkg: grunt.file.readJSON('package.json'),


    // concatenate files
    concat: {
      build: {
        src: [
          'Content/Scripts/Vendor/jquery/jquery.js',
          'Content/Scripts/Vendor/jquery/jquery-migrate.js',
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
        dest: 'Release/app.js'
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
          environment: 'development'
        }
      },
      release: {
        options: {
          sassDir: 'Content/Sass',
          cssDir: 'Release',
          imagesDir: 'Content/Images',
          fontDir: 'Content/Fonts',
          javascriptsDir: 'Content/Scripts',
          outputStyle: 'compressed',
          environment: 'production'
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
      dist: {
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


  // build task, which generates the development JS
  grunt.registerTask('build', [ 'concat', 'compass:build' ]);

  // distribution task
  grunt.registerTask('release', [ 'concat', 'uglify', 'compass:release' ]);

};
