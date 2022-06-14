<template>
  <scrapper-form />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import ScrapperForm from "./components/ScrapperForm.vue";

export default defineComponent({
  name: "App",
  components: {
    ScrapperForm,
  },
});
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  margin-top: 60px;
}
</style>
