import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import 'leaflet/dist/leaflet.css'
import { DiscoverDrawerProvider } from './components/context/provider/DiscoverDrawerProvider.jsx'
import { DialogProvider } from './components/context/provider/DialogProvider.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <DialogProvider>
      <DiscoverDrawerProvider>
        <App />
      </DiscoverDrawerProvider>
    </DialogProvider>
  </StrictMode>,
)
