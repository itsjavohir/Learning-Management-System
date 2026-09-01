import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { userApi } from '../api/userApi';

export const userKeys = {
    all: ['users'],
};

export const useUsers = () =>
    useQuery({
        queryKey: userKeys.all,
        queryFn: userApi.getAll,
    });

export const useCreateUser = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: userApi.create,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: userKeys.all });
        },
    });
};

export const useDeleteUser = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: userApi.delete,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: userKeys.all });
        },
    });
};
