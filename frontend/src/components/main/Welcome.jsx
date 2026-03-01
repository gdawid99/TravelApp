import { Box, Typography } from "@mui/material"

export const Welcome = () => {
    return(
        <>
            <Box 
                height='550px' 
                width='100vw'
                sx={{
                    position: 'relative', 
                    overflow: 'hidden',
                    margin: '20px 0'
                }}
            >
                <Box
                    component="img"
                    src="public/images/welcome_images/peru.jpg"
                    alt="Welcome Image"
                    sx={{
                        width: '100%',
                        height: '100%',
                        objectFit: 'cover'
                    }}
                />
                <Typography
                    variant="h2"
                    color="#ebc8ac"
                    sx={{
                        position: 'absolute',
                        top: '50%',
                        left: '50%',
                        transform: 'translate(-50%, -50%)',
                        textAlign: 'center',
                        WebkitTextStroke: '2px black'
                    }}
                >
                    Welcome on Travel App site!
                </Typography>
            </Box>
        </>
    )
}