<template>
  <div>
    <h1>Test</h1>
    <ul>
      <li v-for="post in posts" :key="post.name">
        <div>
          <h2>{{ post.name }}</h2>
          <p>{{ post.content }}</p>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import axios from "axios";
import { getInstance } from "@/auth";

export default {
  name: "posts",
  data() {
    return {
      posts: []
    };
  },

  created() {
    this.init(this.loadTokenIntoStore);
  },
  methods: {
    init(fn) {
      var instance = getInstance();
      instance.$watch("loading", loading => {
        if (loading === false) {
          fn(instance);
        }
      });
    },
    async loadTokenIntoStore(instance) {
      if (instance.isAuthenticated) {
        await instance.getTokenSilently().then((authToken) => {
          this.getPosts(authToken);
        });
      }
      else {
        this.getPosts();
      }
    },
    async getPosts(authToken) {
      let headers = {}

      if (authToken !== null) {
        headers["Authorization"] = `Bearer ${authToken}`
      }

      const { data } = await axios.get("https://localhost:5001/posts", {
        headers: headers
      });

      this.posts = data;
    }
  }
}
</script>
