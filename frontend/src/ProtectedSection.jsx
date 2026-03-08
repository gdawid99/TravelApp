import { Button, Typography } from "@mui/material";
import { useDialogContext } from "./hooks/useDialogContext";
import { useAuth } from "./hooks/useAuth";

export const ProtectedSection = ({ children }) => {
    const { user } = useAuth();
    const { openDialog } = useDialogContext();

    if (!user) {
        return (
            <div>
                <Typography>This section requires logging in.</Typography>
                <Button onClick={() => openDialog('loginDialog')}>Log in now</Button>
            </div>
        )
    }

    console.log(`For: ${user.name}`);
    return children;
}