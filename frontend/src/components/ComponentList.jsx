import { Box, Button, Typography } from "@mui/material"
import { PlaceComponent } from "./PlaceComponent"
import { useRef, useState } from "react"
import { motion, useAnimationFrame } from "framer-motion";

const Places = [
{
    name: 'Wawel, Poland',
    image: 'public/images/cards/wawel.jpg'
},
{
    name: 'Hradčany, Czech Republic',
    image: 'public/images/cards/hradcany.jpg'
},
{
    name: 'Taj Mahal, India',
    image: 'public/images/cards/taj_mahal.jpg'
}
];

export const ComponentList = () => {
    const defaultSpeed = 0.1;
    const increasedSpeed = 0.6;
    const [speed, setSpeed] = useState(defaultSpeed);
    const [isHovered, setIsHovered] = useState(false);
    const offsetX = useRef(0);
    const animationRef = useRef(0);
    const gap = 20;
    
    useAnimationFrame((_, delta) => {
        if (!isHovered) {
            offsetX.current -= delta * speed;
            const looped = offsetX.current % (3 * (400 + gap + 10));
            animationRef.current.style.transform = `translateX(${looped}px)`;
        }
    })

    return(
        <>
            <Box sx={{display: 'flex', justifyContent: 'space-between', margin:'20px 0'}}>
                <Box width='100px' height='100px' sx={{display: 'flex', justifyContent:'center', margin:'auto 0'}}>
                    <Button onMouseDown={() => setSpeed(-increasedSpeed)} onMouseUp={() => setSpeed(defaultSpeed)}>
                        <Typography variant='h4' sx={{margin:'auto'}}>
                            &#8249;
                        </Typography>
                    </Button>
                </Box>
                <Box sx={{display: 'flex', overflow: "hidden", whiteSpace: "nowrap" }}>
                    <motion.div ref={animationRef} onMouseEnter={() => setIsHovered(true)} onMouseLeave={() => setIsHovered(false)} style={{display: 'flex', gap:`${gap}px` }}>
                        {
                            [...Places, ...Places, Places[0], Places[1]].map((element, index) => 
                                <PlaceComponent key={index} img={element.image} imgTitle={element.name}/>
                            )
                        }
                    </motion.div>
                </Box>
                <Box width='100px' height='100px' sx={{display: 'flex', justifyContent:'center', margin:'auto 0'}}>
                    <Button onMouseDown={() => setSpeed(increasedSpeed)} onMouseUp={() => setSpeed(defaultSpeed)}>
                        <Typography variant='h4' sx={{margin:'auto'}}>
                            &#8250;
                        </Typography>
                    </Button>
                </Box>
            </Box>
        </>
    )
}