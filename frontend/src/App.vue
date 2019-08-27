<template>
  <div id="app">
       
    <el-row>
      <el-col :span="24">
        <h1 class="page-title"> <img alt="Vue logo" src="./assets/logo.png" width="50px"> Restaurants</h1>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-input
          placeholder="Search restaurants by location..."
          v-model="textSearch"
          class="input-with-select"
          @keyup.enter.native="search"
        >
          <el-button slot="append" icon="el-icon-search" v-on:click="search"></el-button>
        </el-input>
      </el-col>
    </el-row>
    <div v-loading="loading">
      <el-row v-if="results.length > 0">
        <el-col :span="24" v-for="(o) in results" :key="o.id">
          <el-card class="card" shadow="hover">
            <h3><i class="el-icon-s-flag"></i> {{o.name}}</h3>
            <span>
              <i class="el-icon-map-location"></i>
              {{o.geometry.location.lat}}, {{o.geometry.location.lng}}
            </span>
          </el-card>
        </el-col>
      </el-row>
      <el-row v-else>
        <el-col :span="24">
          <el-card class="card" shadow="none">
            <span>Not found restaurants.</span>
          </el-card>
        </el-col>
      </el-row>
    </div>

    <el-alert
      v-if="error"
      class="alert"
      title="error alert"
      type="error"
      description="Network Error"
      show-icon
      :closable="false"
    ></el-alert>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "app",
  components: {},
  data() {
    return {
      results: [],
      textSearch: "Bangsue",
      error: "",
      loading: true
    };
  },
  mounted() {
    this.loading = true;
    axios
      .get("http://localhost:5000/api/googleapi/GetRestaurants/Bangsue")
      .then(response => (this.results = response.data.results))
      .catch(error => {
        this.error = error;
      })
      .finally((this.loading = false));
  },
  methods: {
    search: function() {
      this.textSearch = this.textSearch.trim();
      if (this.textSearch) {
        this.loading = true;
        axios
          .get(
            "http://localhost:5000/api/googleapi/GetRestaurants/" + this.textSearch
          )
          .then(response => (this.results = response.data.results))
          .catch(error => {
            this.error = error;
          })
          .finally((this.loading = false));
      }
    }
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  max-width: 1400px;
  margin: 2vh auto;
}
.page-title {
  text-align: center;
}
.card {
  margin-top: 2vh;
}
.alert {
  margin-top: 2vh;
}
</style>
