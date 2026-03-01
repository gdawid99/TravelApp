import { Card, CardContent, CardMedia, Stack, Typography } from "@mui/material"

export const PlaceComponent = ({ img, imgTitle }) => {
    return(
        <Card sx={{minWidth:'400px', minHeight:'400px', border: '5px solid black'}}>
            <CardMedia
                sx={{ width:'100%', height:'100%'}}
                image={img}
                title={imgTitle}
            >
                <CardContent>
                    {imgTitle}
                </CardContent>
            </CardMedia>
        </Card>
    )
}