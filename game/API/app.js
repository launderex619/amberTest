const path = require('path');
const express = require('express');
const morgan = require('morgan');
const rateLimit = require('express-rate-limit');
const helmet = require('helmet');
const mongoSanitize = require('express-mongo-sanitize');
const xss = require('xss-clean');
const hpp = require('hpp');
const compression = require('compression');

const AppError = require('./utils/appError');
const globalErrorHandler = require('./controllers/errorController');
const leaderboardRouter = require('./routes/leaderboardRoutes');
const levelRouter = require('./routes/levelRoutes');

const app = express();

//serving static files
app.use(express.static(path.join(__dirname, 'public/img')));

if (process.env.NODE_ENV === 'development') {
  app.use(morgan('dev'));
}

// global middlewares
const limiter = rateLimit({
  max: 100, // limit each IP to 100 requests per windowMs
  windowMs: 60 * 60 * 1000, // 60 minutes
  message: 'Too many requests from this IP, please try again in an hour!'
});
app.use(helmet()); // security http
app.use('/api', limiter); // limit requests
app.use(express.json({ limit: '10kb' })); // reading data from body into req.body
app.use(express.urlencoded({ extended: true, limit: '10kb' }));
// data sanitization against NoSQL query injection
app.use(mongoSanitize());
// data sanitization against XSS
app.use(xss());
// prevent parameter polution
app.use(
  hpp({
    whitelist: ['score']
  })
);

app.use(compression());

app.use((req, res, next) => {
  req.requestTime = new Date().toISOString();
  next();
});

// Routes
app.use('/api/v1/level', levelRouter);
app.use('/api/v1/leaderboard', leaderboardRouter);

// Not found route at all
app.all('*', (req, res, next) => {
  next(new AppError(`Cant find ${req.originalUrl} on this server!`, 404));
});

app.use(globalErrorHandler);

module.exports = app;
