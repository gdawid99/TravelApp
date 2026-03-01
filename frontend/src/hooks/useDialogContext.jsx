import { useContext } from "react";
import { DialogContext } from "../components/context/DialogContext";

export const useDialogContext = () => useContext(DialogContext);