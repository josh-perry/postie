import axios from 'axios';

export default {
  state: {
    title: "Unknown board",
    createdBy: "Unknown",
    createdDateTime: null,
    url: "",
    description: "No description"
  },
  mutations: {
    SET_BOARD(state, board) {
      state.title = board.title;
      state.createdBy = board.createdBy;
      state.createdDateTime = board.createdDateTime;
      state.url = board.url;
      state.description = board.description;
    }
  },
  actions: {
    async retrieveBoardDetails(context, boardUrl) {
      console.log(`Fetching board data for '${boardUrl}'`);
      if (boardUrl == context.state.url) {
        console.log(`Already got board data for '${boardUrl}', skipping request`);
        return;
      }

      const { data } = await axios.get(`board/${boardUrl}`)
      context.commit("SET_BOARD", data)
    }
  }
};