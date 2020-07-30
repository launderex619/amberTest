const express = require('express');
const leaderboarController = require('../controllers/leaderboardController');

const router = express.Router();

router
  .get(
    '/:level_id',
    leaderboarController.addFilter,
    leaderboarController.getScores
  )
  .post('/:level_id', leaderboarController.addScore);

module.exports = router;
