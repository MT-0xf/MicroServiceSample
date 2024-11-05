import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue';
import Upload from '../views/Upload.vue';
import PostImageList from '@/views/PostImageList.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: Login
    },
    {
      path: '/upload',
      name: 'upload',
      component: Upload
    },
    {
      path: '/postImageList',
      name: 'postImageList',
      component: PostImageList
    }
  ]
});

export default router
