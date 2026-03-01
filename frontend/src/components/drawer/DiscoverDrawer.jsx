import { Drawer } from "@mui/material"
import { DiscoverPanel } from "../DiscoverPanel"
import { useDiscoverDrawer } from "../../hooks/useDiscoverDrawer";

export const DiscoverDrawer = () => {
    const { open, setOpen } = useDiscoverDrawer();

    return(
        <Drawer
          anchor='left'
          open={open}
          onClose={() => setOpen(false)}
          slotProps={{paper: {
            sx: {
              backgroundColor:'#cafed7'
            }
          }}}
        >
          <DiscoverPanel/>
        </Drawer>
    )
}