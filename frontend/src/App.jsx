import { Box } from "@mui/material"
import { Header } from "./components/main/Header"
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { ComponentList } from "./components/ComponentList";
import { Welcome } from "./components/main/Welcome";
import { Map } from "./components/main/Map";
import { LoginDialog } from "./components/dialog/Logindialog";
import { DiscoverDrawer } from "./components/drawer/DiscoverDrawer";
import { SignupDialog } from "./components/dialog/SignupDialog";

const theme = createTheme({
  palette: {
    primary: {
      light: '#6fbf73',
      main: '#2f9333',
      dark: '#195a1c',
      contrastText: '#fff',
    },
    secondary: {
      light: '#ff6333',
      main: '#ff3d00',
      dark: '#b22a00',
      contrastText: '#000',
    },
    tertiary: {
      light: '#ff6333',
      main: '#ff3d00',
      dark: '#b22a00',
      contrastText: '#000',
    },
  },
});

function App() {

  return (
    <>
      <ThemeProvider theme={theme}>
        <DiscoverDrawer/>
        <LoginDialog/>
        <SignupDialog/>
        <Box>
          <Header/>
          <Welcome/>
          <ComponentList/>
          <Map/>
        </Box>
      </ThemeProvider>
    </>
  )
}

export default App
