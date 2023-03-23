import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
//import copy from 'rollup-plugin-copy';
import copy from 'rollup-plugin-copy-glob';


// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()/*, copy([
    { files: 'dist/*', dest: '../CSVReader/wwwroot/assets' }
  ], { watch: 'dist/*' })*/
  ],
  build: {
//Для Development сборки
    //sourcemap: true,
    minify: false,
    outDir: '../CSVReader/wwwroot/assets',
    rollupInputOptions: {
      treeshake: false
    },
    cacheDir: '.vite-cache',
    brotliSize: false, // отключение сжатия с помощью brotli
//Конец для Development сборки
    rollupOptions: {
      input: {
        main: 'src/main.js',
      },
      output: {
        entryFileNames: `[name].js`,
        chunkFileNames: `[name].js`,
        assetFileNames: `[name].[ext]`
      }
    }
  },
})
