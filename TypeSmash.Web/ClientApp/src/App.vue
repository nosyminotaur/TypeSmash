<template>
    <div id="app">
        <Navbar />
        <transition name="fade"
                    mode="out-in">
            <router-view></router-view>
        </transition>
    </div>
</template>

<script>
import Navbar from './components/Navbar.vue'

export default {
  name: 'app',
  components: {
    Navbar
  }
}
</script>

<style>
html, body, #app {
  height: 100%;
  text-align: center;
}
.fade-enter-active,
.fade-leave-active {
  transition-duration: 0.3s;
  transition-property: opacity;
  transition-timing-function: ease;
}

.fade-enter,
.fade-leave-active {
  opacity: 0
}
</style>
