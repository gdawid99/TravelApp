import { Button, Stack, TextField, Typography } from "@mui/material"
import { useCreateUser } from "../../hooks/api/useUsers"
import { useState } from "react";
import { useDialogContext } from "../../hooks/useDialogContext";

export const SignupForm = () => {
    const { mutate, loading } = useCreateUser();
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { closeDialog } = useDialogContext();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await mutate({ Name: name, Email: email, Password: password });
        }
        catch (err) {
            console.log(err);
        }
        finally {
            closeDialog('signupDialog');
        }
    };


    return(
        <>
            <Stack height={'100%'} width={'100%'} direction={'column'} alignItems={"center"} justifyContent={"space-evenly"}>
                <Typography variant="h3">Create an account</Typography>
                <Stack spacing={4} width={'80%'} direction={'column'}>
                    <TextField color="secondary" label="Name" variant="outlined" onChange={(e) => setName(e.target.value)}/>
                    <TextField color="secondary" label="Email" variant="outlined" onChange={(e) => setEmail(e.target.value)}/>
                    <TextField color="secondary" label="Password" variant="outlined" onChange={(e) => setPassword(e.target.value)}/>
                </Stack>
                <Button size="large" variant="contained" disabled={loading} onClick={handleSubmit}>{loading ? 'Loading...' : 'Create'}</Button>
            </Stack>
        </>
    )
}