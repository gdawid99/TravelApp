import { Dialog, Slide } from "@mui/material"
import { useDialogContext } from "../../hooks/useDialogContext"
import React from "react";

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="down" ref={ref} {...props} />;
});

export const AuthenticationDialog = ({ children, dialogKey }) => {
    const { dialog, toggleDialog } = useDialogContext();
    
    return(
        <>
            <Dialog
                open={!!dialog[dialogKey]}
                onClose={() => toggleDialog(dialogKey)}
                slots={{
                    transition: Transition
                }}
                slotProps={{
                    paper: {
                        sx: {
                            backgroundColor:'#cafed7',
                            width: '500px',
                            height: '700px'
                        }
                    }
                }}
            >
                {children}
            </Dialog>
        </>
    )
}