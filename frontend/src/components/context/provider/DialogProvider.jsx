import { useState } from "react";
import { DialogContext } from "../DialogContext";

export const DialogProvider = ({ children }) => {
    const [dialog, setDialog] = useState(false);

    const openDialog = (id) => setDialog((prev) => ({...prev, [id]: true}));

    const closeDialog = (id) => setDialog((prev) => ({...prev, [id]: false}));

    const toggleDialog = (id) => setDialog((prev) => ({...prev, [id]: !prev[id]}));

    return(
        <DialogContext.Provider value={{dialog, openDialog, closeDialog, toggleDialog}}>
            {children}
        </DialogContext.Provider>
    );
}