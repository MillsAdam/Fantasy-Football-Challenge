module.exports = {
    transform: {
        '^.+\\.jsx?$': ['babel-jest', {presets: ['@babel/preset-env', '@babel/preset-react']}],
      },
      
      transformIgnorePatterns: [
        '/node_modules/(?!axios).+\\.js$'
      ],
  };
  