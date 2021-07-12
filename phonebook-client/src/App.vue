<template>
  <div id="app">
    <Phonebook />
  </div>
</template>

<style>
  @import './assets/styles/styles.css';
</style>

<script>
import Phonebook from './components/Phonebook.vue'

export default {
  name: 'App',
  components: {
    Phonebook
  }
}
</script>
