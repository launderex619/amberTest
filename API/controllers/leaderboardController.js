const Leaderboard = require('../models/leaderboardModel');
const Level = require('../models/levelModel');
const catchAsync = require('../utils/catchAsync');
const factory = require('./handlerFactory');
const AppError = require('../utils/appError');

exports.addScore = catchAsync(async (req, res, next) => {
  if (req.params.level_id) {
    req.body.level_id = req.params.level_id;
  } else {
    next(new AppError('Level needed and not granted', 400));
  }
  const level = await Level.findById(req.params.level_id);
  if (!level) {
    next(new AppError('Level granted not found', 400));
  }
  const doc = await Leaderboard.create(req.body);
  res.status(201).json({
    status: 'success',
    data: {
      data: doc
    }
  });
});

exports.addFilter = (req, res, next) => {
  req.query.limit = '5';
  req.query.sort = '-score';
  next();
};

exports.getScores = factory.getAll(Leaderboard);
