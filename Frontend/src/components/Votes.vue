<template>
  <div class="container">
    <div class="upvotes">
      <span>{{ upVotes }}</span>
      <a href="#" @click.prevent="postVote(true)" v-bind:class="{ selected: voted === true }">
        :)
      </a>
    </div>

    <div>
      <a href="#" @click.prevent="postVote(false)" v-bind:class="{ selected: voted === false }">
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
  data() {
    return {
      voted: null
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

      const { data } = await axios.post(`https://localhost:5001/vote/post/${this.post.id}`, json, {
        headers: headers,
        "Content-Type": "application/json"
      });

      this.post.upVotes = data.upVotes
      this.voted = up
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

.selected {
  background-color: #F06543;
}

a {
  height: 32px;
  width: 32px;
  border-radius: 50%;

  margin: 4px;

  display: inline-block;

  background-color: #F0654354;
  color: white;

  font-size: 18px;
  text-decoration: none;
  text-align: center;
  vertical-align: middle;
}
</style>
