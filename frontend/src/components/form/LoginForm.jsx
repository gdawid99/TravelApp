import { Box, Button, Link, Stack, TextField, Typography, useTheme } from "@mui/material"
import { useDialogContext } from "../../hooks/useDialogContext";

export const LoginForm = () => {
    const theme = useTheme();
    const { openDialog, closeDialog } = useDialogContext();

    const changeDialog = () => {
        closeDialog('loginDialog');
        openDialog('signupDialog');
    }

    return(
        <>
            <Stack height={'100%'} width={'100%'} direction={'column'} alignItems={"center"} justifyContent={"space-evenly"}>
                <Typography variant="h3">Welcome</Typography>
                <Stack spacing={4} width={'80%'} direction={'column'}>
                    <TextField color="secondary" label="Email" variant="outlined"/>
                    <TextField color="secondary" label="Password" variant="outlined"/>
                    <Link onClick={changeDialog} component='button' underline="none" variant="body2" sx={{color:theme.palette.secondary.main, display:'flex', justifyContent:'center'}}>You don't have an account? Create it now here!</Link>
                </Stack>
                <Button size="large" variant="contained">Login</Button>
            </Stack>
        </>
    )
}