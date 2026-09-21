import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { groupApi } from '../api/groupApi';

export const groupKeys = {
    all: ['groups'],
};

export const useGroups = () =>
    useQuery({
        queryKey: groupKeys.all,
        queryFn: async () => {
            const data = await groupApi.getAll();
            return Array.isArray(data) ? data : (data?.data ?? []);
        },
    });

export const useCreateGroup = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: groupApi.create,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: groupKeys.all });
        },
    });
};

export const useUpdateGroup = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: groupApi.update,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: groupKeys.all });
        },
    });
};

export const useDeleteGroup = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: groupApi.delete,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: groupKeys.all });
        },
    });
};
