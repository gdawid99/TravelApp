import { useState } from "react";
import { usersApi } from "../../services/api";

export const useCreateUser = () => {
    const [result, setResult] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const createUser = async (data) => {
        setLoading(true);
        setError(null);
        try {
            const res = await usersApi.create(data);
            setResult(res.data);
            return res.data
        }
        catch (err) {
            setError(err);
            throw err;
        }
        finally {
            setLoading(false);
        }
    };

    return {mutate: createUser, result, loading, error};
}