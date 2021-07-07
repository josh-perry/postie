<template>
  <div class="container">
    <div class="upvotes">
      <span>{{ upVotes }}</span>
      <a href="#" @click.prevent="postVote(true)">
        :)
      </a>
    </div>

    <div>
      <a href="#" @click.prevent="postVote(false)">
        :(
      </a>
    </div>
  </div>
</template>

<script>
import axios from "axios"
import { store } from "../store/store"

export default {
  name: "Votes",
  props: {
    post: {
      type: Object,
      default: null
    }
  },
  computed: {
    upVotes() {
      return this.post.upVotes
    }
  },
  methods: {
    async postVote(up) {
      if (store.state.token === null) {
        return
      }

      let headers = {
        Authorization: `Bearer ${store.state.token}`
      }

      let json = {
        up
      }

      await axios.post(`https://localhost:5001/vote/post/${this.post.id}`, json, {
        headers: headers,
        "Content-Type": "application/json"
      });
    }
  }
}
</script>

<style scoped>
.container div {
  display: inline-block;
}

.upvotes {
  min-width: 80px;
  text-align: right;
}

a {
  height: 32px;
  width: 32px;
  border-radius: 50%;

  margin: 4px;

  display: inline-block;

  background-color: #F06543;
  color: white;

  font-size: 18px;
  text-decoration: none;
  text-align: center;
  vertical-align: middle;
}
</style>
