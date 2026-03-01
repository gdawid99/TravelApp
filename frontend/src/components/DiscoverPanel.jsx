import { Button, Collapse, List, ListItemButton, ListItemText, Stack, Typography } from "@mui/material"
import { useState } from "react"

const EuropeCountries = ["UK", "France", "Italy", "Spain", "Portugal", "Germany", "Greece", "Poland", "Switzerland", "Sweden", "Norway", "Russia", "Austria", "Ukraine", "Lithuania", "Czech Republik", "Slovakia", "Slovenia", "Belgium", "Netherlands", "Finland", "Croatia", "Hungary", "Romania", "Bulgaria", "Denmark", "Ireland"];
const AsiaCountries = ["China", "Japan", "Türkiye", "Georgia", "South Korea", "India", "Saudi Arabia", "UAE", "Israel", "Lebanon", "Iran", "Pakistan", "Kazakhstan", "Thailand", "Indonesia", "Malaysia", "Singapore"];
const AfricaCountries = ["Egypt", "Tunisia", "Algeria", "Morocco", "Ethiopia", "South Africa", "Kenya", "Tanzania", "Nigeria", "Mauritius", "Seychelles"];
const NorthAmericaCountries = ["USA", "Canada", "Mexico", "Cuba", "Puerto Rico", "Dominican Republic", "Jamaica", "Guatemala", "Panama"];
const SouthAmericaCountries = ["Brazil", "Argentina", "Chile", "Uruguay", "Peru", "Bolivia", "Ecuador", "Colombia", "Venezuela"];
const AustraliaAndOceaniaCountries = ["Australia", "New Zealand", "Fiji", "Papua New Guinea", "Vanuatu", "Samoa", "Palau", "Kiribati"];

export const DiscoverPanel = () => {
    return(
        <>
            <Stack direction={'column'} width={'300px'}>
                <Typography color="secondary" fontStyle={'italic'} variant="h3" margin={'20px auto'}>Discover</Typography>
                <DiscoverButtonAndList list={EuropeCountries}>
                    Europe
                </DiscoverButtonAndList>
                <DiscoverButtonAndList list={AsiaCountries}>
                    Asia
                </DiscoverButtonAndList>
                <DiscoverButtonAndList list={AfricaCountries}>
                    Africa
                </DiscoverButtonAndList>
                <DiscoverButtonAndList list={NorthAmericaCountries}>
                    North America
                </DiscoverButtonAndList>
                <DiscoverButtonAndList list={SouthAmericaCountries}>
                    South America
                </DiscoverButtonAndList>
                <DiscoverButtonAndList list={AustraliaAndOceaniaCountries}>
                    Australia and Oceania
                </DiscoverButtonAndList>
            </Stack>
        </>
    )
}

const DiscoverButtonAndList = ({children, list}) => {
    const [open, setOpen] = useState(false);

    return(
        <>
            <Button color='secondary' onClick={() => setOpen(prev => !prev)} sx={{height:'60px'}}>
                {children}
            </Button>
            <Collapse in={open} timeout={'auto'} unmountOnExit>
                <NestedList list={list}/>
            </Collapse>
        </>
    )
}

const NestedList = ({ list }) => {
    return(
        <>
            <List>
                { list.map((country, index) => (
                    <ListItemButton key={index}> 
                        <ListItemText primary={country} />
                    </ListItemButton>
                ))}   
            </List>
        </>
    )
}