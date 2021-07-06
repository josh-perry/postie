<template>
  <div>
    <ul>
      <li v-for="crumb in crumbs" v-bind:key="crumb">
        <a v-if="crumb.link != null" :href="crumb.link">{{ crumb.text }}</a>
        <span v-if="crumb.link == null">{{ crumb.text }}</span>
      </li>
    </ul>
  </div>
</template>

<script>
class Crumb {
  constructor(text, link) {
    this.link = link
    this.text = text
  }
}

function create_crumbs(route) {
  let crumbs = []
  crumbs.push(new Crumb("Home", "/"))

  let split = route.toLowerCase().split("/").filter((x) => x)

  if (split.length == 0) {
    return crumbs
  }

  if (split[0] == "board" && split[1] != null) {
    crumbs.push(new Crumb(split[1], `/board/${split[1]}`))

    if (split[2] != null) {
      crumbs.push(new Crumb(split[2], null))
    }
  }

  if (split[0] == "user" && split[1] != null) {
    crumbs.push(new Crumb("User", null))
    crumbs.push(new Crumb(split[1], split[1]))
  }

  if (crumbs.length > 1) {
    crumbs[crumbs.length-1].link = null
  }

  return crumbs
}

export default {
  name: "Breadcrumb",
  computed: {
    crumbs() {
      return create_crumbs(this.$route.path)
    }
  }
}
</script>

<style scoped>
ul {
  list-style-type: none;
  overflow: hidden;
}

li {
  float: left;
  display: inline;
  max-width: 250px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}

li+li:before {
  padding: 8px;
  content: "/\00a0";
}
</style>
