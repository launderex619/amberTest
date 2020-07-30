const multer = require('multer');
const sharp = require('sharp');
const Level = require('../models/levelModel');
const AppError = require('../utils/appError');
const catchAsync = require('../utils/catchAsync');
const factory = require('./handlerFactory');

const multerStorage = multer.memoryStorage();

const multerFilter = (req, file, cb) => {
  if (file.mimetype.startsWith('image')) {
    cb(null, true);
  } else {
    cb(new AppError('Not an image! Please upload only images', 400), false);
  }
};

const upload = multer({
  storage: multerStorage,
  fileFilter: multerFilter
});

exports.uploadLevelImage = upload.single('image');

exports.resizeLevelImage = catchAsync(async (req, res, next) => {
  if (!req.file) {
    return next(new AppError('Must have an image', 400));
  }
  req.file.filename = `${req.body.name}.jpeg`;
  req.body.image = req.file.filename;

  await sharp(req.file.buffer)
    .resize(500, 500)
    .toFormat('jpeg')
    .jpeg({ quality: 90 })
    .toFile(`public/img/${req.file.filename}`);

  next();
});

exports.createLevel = factory.createOne(Level);
exports.getLevels = factory.getAll(Level);
exports.getLevel = factory.getOne(Level);
