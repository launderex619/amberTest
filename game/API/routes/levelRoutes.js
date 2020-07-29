const express = require('express');
const levelController = require('../controllers/levelController');

const router = express.Router();

router
  .get('/:id', levelController.getLevel)
  .get('/', levelController.getLevels)
  .post(
    '/',
    levelController.uploadLevelImage,
    levelController.resizeLevelImage,
    levelController.createLevel
  );

module.exports = router;
