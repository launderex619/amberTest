const mongoose = require('mongoose');

const leaderboardSchema = new mongoose.Schema({
  level_id: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'levels',
    unique: false,
    required: [true, 'Needed level to add the score']
  },
  player: {
    type: String,
    required: [true, 'Player must have a name!']
  },
  score: {
    type: Number,
    required: [true, 'You must have an score!']
  }
});

const Leaderboard = mongoose.model('leaderboards', leaderboardSchema);

module.exports = Leaderboard;
