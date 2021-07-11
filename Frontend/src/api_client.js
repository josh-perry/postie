import { store } from "./store/store"

import axios from "axios"

class ApiClient {
  getDefaultHeaders() {
    let headers = {}

    if (store.state.token !== null) {
      headers["Authorization"] = `Bearer ${store.state.token}`
    }

    return headers
  }

  async get(url) {
    const { data } = await axios.get(url, {
      headers: this.getDefaultHeaders()
    });

    console.log(data)
    return data
  }

  async getTopPosts() {
    return this.get("https://localhost:5001/post/top");
  }

  async getPostsForBoard(boardUrl) {
    return this.get(`https://localhost:5001/post/board/${boardUrl}`);
  }
}

export default new ApiClient();
