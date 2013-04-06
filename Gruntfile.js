

module.exports = function(grunt)
{

  // configuration for grunt
  grunt.initConfig({

    // loads the package configuration file
    pkg: grunt.file.readJSON('package.json'),


    // concatenate files
    concat: {
      build: {
        src: [],
        dest: ''
      }
    },


    // uglifier options
    uglify: {
      options: {
        banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n'
      },
      build: {
        src: '',
        dest: ''
      }
    },


    // compile compass files
    compass: {
      build: {
        options: {
          sassDir: '',
          cssDir: '',
          imagesDir: '',
          fontDir: '',
          javascriptsDir: '',
          outputStyle: 'expanded',
          environment: 'development'
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
  grunt.registerTask('dist', [ 'concat', 'uglify', 'compass:dist' ]);

};
