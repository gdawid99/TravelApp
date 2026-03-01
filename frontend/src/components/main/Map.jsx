import { Box } from "@mui/material"
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet"

export const Map = () => {
    return(
        <Box width={'95%'} height={'900px'} sx={{border:'2px solid', margin:'80px auto'}}>
            <MapContainer
                center={[50.0550, 19.9550]}
                zoom={13}
                style={{ height: "900px", width: "100%" }}
            >
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                {/* <Marker position={[52.2297, 21.0122]}>
                    <Popup>Warszawa</Popup>
                </Marker>    */}
            </MapContainer>
        </Box>
    )
}