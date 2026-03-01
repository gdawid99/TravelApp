import { useContext } from "react";
import { DiscoverPanelContext } from "../components/context/DiscoverPanelContext";

export const useDiscoverDrawer = () => useContext(DiscoverPanelContext);