import { useState } from "react";
import { DiscoverPanelContext } from "../DiscoverPanelContext";

export const DiscoverDrawerProvider = ({ children }) => {
    const [open, setOpen] = useState(false);

    return(
        <DiscoverPanelContext.Provider value={{open, setOpen}}>
            {children}
        </DiscoverPanelContext.Provider>
    );
};