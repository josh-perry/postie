import axios from "axios"

class ApiClient {
  constructor() {
    this.baseUrl = "https://localhost:5001"
  }

  getDefaultHeaders() {
    let headers = {}

    //if (store.state.token !== null) {
    //  headers["Authorization"] = `Bearer ${store.state.token}`
    //}

    return headers
  }

  async get(url) {
    const { data } = await axios.get(`${this.baseUrl}/${url}`, {
      headers: this.getDefaultHeaders()
    })

    return data
  }

  async post(url, body) {
    const { data } = await axios.post(`${this.baseUrl}/${url}`, body, {
      headers: this.getDefaultHeaders(),
      "Content-Type": "application/json"
    })

    return data
  }

  async getTopPosts() {
    return this.get("post/top")
  }

  async getPostsForBoard(boardUrl) {
    return this.get(`post/board/${boardUrl}`)
  }

  async getUser(username) {
    return this.get(`user/${username}`)
  }

  async postVoteOnPost(id, isUpvote) {
    let json = {
      up: isUpvote
    }

    return this.post(`vote/post/${id}`, json)
  }
}

export default new ApiClient();
