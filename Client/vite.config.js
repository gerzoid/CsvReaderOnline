import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import copy from 'rollup-plugin-copy';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue(),
    copy({
      targets: [
        { src: 'dist/assets', dest: '../CSVReader/wwwroot/' }
      ]
    })
  ],
  build: {
    sourcemap: true,
    rollupOptions: {
      input: {
        main: 'src/main.js',
      },
      output: {
        entryFileNames: `assets/[name].js`,
        chunkFileNames: `assets/[name].js`,
        assetFileNames: `assets/[name].[ext]`
      }
    }
  }
})
