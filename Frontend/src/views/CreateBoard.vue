<template>
  <div>
    <h2>Create a new board</h2>

    <form v-on:submit.prevent="createBoard">
      <fieldset>
        <label>Board name</label>
        <input v-model="form.title" v-on:input="boardNameChanged" type="text" placeholder="Super cool board" />
      </fieldset>

      <fieldset>
        <label>URL</label>
        <input v-model="form.url" type="text" placeholder="super-cool-board" readonly="true" />
      </fieldset>

      <fieldset>
        <label>Description</label>
        <textarea v-model="form.description" placeholder="A place for all my pals to chat!" />
      </fieldset>

      <fieldset>
        <input value="Create" type="submit" />
        <button @click="cancel">Cancel</button>
      </fieldset>
    </form>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      form: {
        title: "",
        url: ""
      }
    };
  },
  methods: {
    async createBoard() {
      let headers = {}

      console.log(this.form)

      const { data } = await axios.put(`board/${this.form.url}`, this.form, {
        headers: headers,
        "Content-Type": "application/json"
      })

      console.log(data)
    },
    boardNameChanged() {
      this.form.url = this.form.title.replaceAll(" ", "-").toLowerCase();
    },
    cancel() {
      this.$router.push("/")
    }
  }
}
</script>

<style scoped>
fieldset {
  border: 0;
  display: flex;
}

label {
  flex: 1;
}

input, textarea {
  flex: 3;
  margin: 8px;
}

button {
  flex: 3;
  margin: 8px;
  padding: 8px;
}

textarea {
  min-height: 120px;
}
</style>
