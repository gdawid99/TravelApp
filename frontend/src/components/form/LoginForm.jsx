import { Box, Button, Link, Stack, TextField, Typography, useTheme } from "@mui/material"
import { useDialogContext } from "../../hooks/useDialogContext";
import { useAuth } from "../../hooks/useAuth";
import { useState } from "react";

export const LoginForm = () => {
    const theme = useTheme();
    const { login, loading } = useAuth();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { openDialog, closeDialog } = useDialogContext();
    

    const changeDialog = () => {
        closeDialog('loginDialog');
        openDialog('signupDialog');
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await login(email, password);
        }
        catch (err) {
            console.log(err);
        }
        finally {
            closeDialog('loginDialog');
        }
    }

    return(
        <>
            <Stack height={'100%'} width={'100%'} direction={'column'} alignItems={"center"} justifyContent={"space-evenly"}>
                <Typography variant="h3">Welcome</Typography>
                <Stack spacing={4} width={'80%'} direction={'column'}>
                    <TextField color="secondary" label="Email" variant="outlined" onChange={(e) => setEmail(e.target.value)}/>
                    <TextField color="secondary" label="Password" type="password" variant="outlined" onChange={(e) => setPassword(e.target.value)}/>
                    <Link onClick={changeDialog} component='button' underline="none" variant="body2" sx={{color:theme.palette.secondary.main, display:'flex', justifyContent:'center'}}>You don't have an account? Create it now here!</Link>
                </Stack>
                <Button size="large" variant="contained" onClick={handleSubmit} disabled={loading}>{loading ? 'Loading...' : 'Login'}</Button>
            </Stack>
        </>
    )
}