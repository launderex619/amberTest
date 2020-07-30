const mongoose = require('mongoose');

const levelSchema = new mongoose.Schema({
  name: {
    type: String,
    unique: true,
    required: [true, 'Please tell us your name!']
  },
  image: {
    type: String,
    default: 'default.jpg'
  },
  index: {
    type: Number,
    required: [true, 'Level needs order!']
  },
  description: {
    type: String,
    required: [true, 'Level needs information about it!']
  }
});

const Level = mongoose.model('levels', levelSchema);

module.exports = Level;
