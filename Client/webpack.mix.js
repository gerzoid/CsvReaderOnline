const mix = require('laravel-mix');

mix.js('src/firstpage.js', '../CSVReader/wwwroot/dist')
   .vue()
   .sass('src/assets/style.scss', '')
   .setPublicPath('../CSVReader/wwwroot/dist');

mix.js('src/editor.js', '../CSVReader/wwwroot/dist')
   .vue()
   .sass('src/assets/editor.scss', '')
   .setPublicPath('../CSVReader/wwwroot/dist');