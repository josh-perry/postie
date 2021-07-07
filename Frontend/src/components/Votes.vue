<template>
  <div class="container">
    <div class="upvotes">
      <span>{{ upvotes }}</span>
      <a href="#">
        :)
      </a>
    </div>

    <div>
      <a href="#">
        :(
      </a>
    </div>
  </div>
</template>

<script>
export default {
  name: "Votes",
  props: {
    upvotes: {
      type: Number,
      default: 0
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
