const mongoose = require('mongoose');
const dotenv = require('dotenv');

process.on('uncaughtException', err => {
  process.exit(1); // code 1 is for unhandled rejection, 0 means success
});

dotenv.config({
  path: './config.env'
});
const app = require('./app');

const DB = process.env.DATABASE.replace(
  '<PASSWORD>',
  process.env.DATABASE_PASSWORD
);
mongoose
  .connect(DB, {
    useNewUrlParser: true,
    useCreateIndex: true,
    useFindAndModify: false
  })
  .then(con => {
    console.log('Connection to mongo succesful');
  });

const port = process.env.PORT || 3000;
const server = app.listen(port, () => {
  console.log(`app running on port ${port}...`);
});

process.on('unhandledRejection', err => {
  server.close(() => {
    process.exit(1); // code 1 is for unhandled rejection, 0 means success
  });
});
