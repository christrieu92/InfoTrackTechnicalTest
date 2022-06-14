<template>
  <form v-on:submit.prevent>
    <div class="sector">
      <label>Keywords:</label>
      <input type="text" v-model="keywords" required />
    </div>

    <div class="sector">
      <label class="urllbl">URL: </label>
      <input type="text" class="url" v-model="url" required />
    </div>

    <div class="submit">
      <button v-on:click="handleSubmit">Get URL Ranking</button>
    </div>
  </form>

  <table-history-ranking updateTable="false" />
</template>

<script lang="ts">
import { AxiosResponse } from "axios";
import { defineComponent } from "vue";
import scrapperRequest from "../models/scrapperRequest";
import scrapperService from "../services/scrapperServices";
import TableHistoryRanking from "./TableHistoryRanking.vue";

export default defineComponent({
  name: "ScrapperForm",
  components: {
    TableHistoryRanking,
  },
  data() {
    return {
      keywords: "",
      url: "",
    };
  },
  methods: {
    handleSubmit(event: any) {
      event.preventDefault();

      const scrapperRequest: scrapperRequest = {
        keywords: this.keywords,
        url: this.url,
      };

      scrapperService
        .postScrapper(scrapperRequest)
        .then((value: AxiosResponse<any, any> | { err: unknown }) => {
          window.location.reload();
        })
        .catch((error: any) => {
          console.log("Request Error" + error);
        });
    },
  },
});
</script>

<style>
form {
  max-width: 420px;
  margin: 30px auto;
  background: lightgray;
  text-align: left;
  padding: 40px;
  border-radius: 10px;
}
label {
  color: black;
  margin: 25px 0 15px;
  font-size: 0.6em;
  text-transform: uppercase;
  letter-spacing: 1px;
  font-weight: bold;
}
input {
  padding: 10px 6px;
  width: 80%;
  box-sizing: border-box;
  border: none;
  border-bottom: 1px solid #ddd;
  color: #555;
  margin-left: 5px;
}

.url {
  padding: 10px 6px;
  width: 89%;
  box-sizing: border-box;
  border: none;
  border-bottom: 1px solid #ddd;
  color: #555;
  margin-left: 5px;
}
.sector {
  display: block;
  margin-bottom: 10px;
}
button {
  background: gray;
  border: 0;
  padding: 10px 20px;
  margin-top: 20px;
  color: white;
  border-radius: 20px;
}
.submit {
  text-align: center;
}
</style>
