import { Button, Stack, TextField, Typography } from "@mui/material"

export const SignupForm = () => {
    return(
        <>
            <Stack height={'100%'} width={'100%'} direction={'column'} alignItems={"center"} justifyContent={"space-evenly"}>
                <Typography variant="h3">Create an account</Typography>
                <Stack spacing={4} width={'80%'} direction={'column'}>
                    <TextField color="secondary" label="Name" variant="outlined"/>
                    <TextField color="secondary" label="Email" variant="outlined"/>
                    <TextField color="secondary" label="Password" variant="outlined"/>
                </Stack>
                <Button size="large" variant="contained">Create</Button>
            </Stack>
        </>
    )
}