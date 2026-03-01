import { SignupForm } from "../form/SignupForm"
import { AuthenticationDialog } from "./AuthenticationDialog"

export const SignupDialog = () => {
    return(
        <AuthenticationDialog dialogKey={'signupDialog'}>
            <SignupForm/>
        </AuthenticationDialog>
    )
}