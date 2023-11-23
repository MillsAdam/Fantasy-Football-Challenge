import { createTheme } from '@mui/material/styles';

// Create a theme instance.
const theme = createTheme({
  palette: {
    primary: {
      main: '#556cd6', // Example color: blue
    },
    secondary: {
      main: '#19857b', // Example color: green
    },
    error: {
      main: '#ff3333', // Example color: red
    },
    background: {
      default: '#fff', // Example color: white
      paper: '#f5f5f5', // Example color: light grey
    },
    text: {
      primary: '#333333', // Dark text color
      secondary: '#555555', // Lighter text color
    },
  },
  typography: {
    fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
    fontSize: 14,
    h1: {
      fontWeight: 500,
      fontSize: '2.2rem',
    },
    h2: {
      fontWeight: 500,
      fontSize: '1.8rem',
    },
    body1: {
      fontSize: '1rem',
    },
    button: {
      textTransform: 'none',
    },
  },
  components: {
    // Example of overriding a component's default style
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 8, // Rounded corners
        },
      },
    },
  },
});

export default theme;