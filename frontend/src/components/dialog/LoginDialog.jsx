import { LoginForm } from "../form/LoginForm"
import { AuthenticationDialog } from "./AuthenticationDialog"

export const LoginDialog = () => {
    return(
        <AuthenticationDialog dialogKey={'loginDialog'}>
            <LoginForm/>
        </AuthenticationDialog>
    )
}