import { Box, Button, TextField } from "@mui/material"
import { useDiscoverDrawer } from "../../hooks/useDiscoverDrawer";
import { useDialogContext } from "../../hooks/useDialogContext";

export const Header = () => {
    const { setOpen } = useDiscoverDrawer();
    const { openDialog } = useDialogContext();

    return(
        <>
            <Box sx={{display: 'flex', justifyContent: 'space-between', padding: '5px'}}>
                <Box width={'1000px'} sx={{display: 'flex', justifyContent: 'left'}}>
                    <Button onClick={()=>setOpen(true)} sx={{marginRight: '20px'}}>Discover</Button>
                    <Button sx={{marginRight: '20px'}}>Popular</Button>
                    <TextField placeholder="Search..." sx={{flexGrow: '1'}}></TextField>
                </Box>
                <Box sx={{display: 'flex', justifyContent: 'right'}}>
                    <Button onClick={() => openDialog('loginDialog')} sx={{marginRight: '20px'}}>Log In</Button>
                    <Button onClick={() => openDialog('signupDialog')}>Sign Up</Button>
                </Box>
            </Box>
        </>
    )
}