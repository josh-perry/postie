<template>
  <div class="outer">
    <NavBar class="navbar"/>

    <div class="container" :class="{ 'vertical': mobile }">
      <div v-if="mobile" class="mobile-menu" :class="{ 'collapse': !mobileMenuOpen }">
        <SideBar :class="{ 'collapse': !mobileMenuOpen && mobile }" />
      </div>

      <div class="main" :class="{ 'collapse': mobileMenuOpen }">
        <div class="route-wrapper" :class="{ 'collapse': mobileMenuOpen }">
          <RouterView />
        </div>
      </div>

      <div v-if="!mobile" class="sidebar">
        <SideBar />
      </div>
    </div> 
  </div>
</template>

<script>
import NavBar from "./components/NavBar";
import SideBar from "./components/SideBar";

export default {
  components: {
    NavBar,
    SideBar
  },
  computed: {
    mobile() {
      return this.$screen.width < 800
    },
    mobileMenuOpen() {
      return this.$store.state.mobileMenu
    }
  },
  created() {
    //this.$store.dispatch("retrieveTokenFromAuth0").then(() => {
    //  this.$store.dispatch("retrieveUserDetails")
    //})
  }
};
</script>

<style scoped>
.container {
  line-height: 1.4;
  display: inline-flex;
  width: 100%;
  height: 100%;
  flex-grow: 1;
}

.container.vertical {
  flex-direction: column;
}

.main {
  flex: 4;
}

.main.collapse {
  flex: 0;
}

.route-wrapper {
  margin: 2em auto;
  margin-top: 0;
 
  max-width: 880px;
  margin: auto;
  padding: 30px;
  background-color: #FFFCF9;
}

.sidebar {
  flex: 1.5;
  background-color: #FFFCF9;
}

.sidebar.collapse {
  display: none;
}

.mobile-menu {
  flex: 1;
  transition: all 0.5s;
}

.mobile-menu.collapse {
  flex: 0;
  transition: all 0.5s;
}

.route-wrapper.collapse {
  display: none;
}

.outer {
  display: flex;
  flex-direction: column;
  height: 100%;
}
</style>
