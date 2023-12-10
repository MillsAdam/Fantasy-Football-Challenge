/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  daisyui: {
    themes: [
      "mytheme",
      {
        mytheme: {
          primary: "#00cffd",
          secondary: "#008100",
          accent: "#408500",
          neutral: "#2b2c26",
          "base-100": "#142730",
          info: "#00ffff",
          success: "#008f51",
          warning: "#af6b00",
          error: "#ff696e",
        },
      },
    ],
  },
  theme: {
    extend: {
      width: {
        '45': '45%',
        '30': '30%',
        '90': '90%',
      },
    },
  },
  plugins: [require('daisyui')],
};
