import { useMutation } from '@tanstack/react-query';
import { authApi, useAuthStore } from '../../../../entities/auth';

export const useLogin = () => {
    const setSession = useAuthStore((state) => state.setSession);

    return useMutation({
        mutationFn: ({ phoneNumber, password }) => authApi.login(phoneNumber, password),
        onSuccess: (data) => {
            setSession(data.user, data.accessToken, data.refreshToken);
        },
    });
};