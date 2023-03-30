const mix = require('laravel-mix');

mix.js('src/firstpage.js', '../CSVReader/wwwroot/dist')
   .vue()
   .sass('src/style.scss', '')
   .setPublicPath('../CSVReader/wwwroot/dist');

mix.js('src/editor.js', '../CSVReader/wwwroot/dist')
   .vue()
   .setPublicPath('../CSVReader/wwwroot/dist');