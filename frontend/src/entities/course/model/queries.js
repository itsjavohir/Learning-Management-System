import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { courseApi } from '../api/courseApi';

export const courseKeys = {
    all: ['courses'],
};

export const useCourses = () =>
    useQuery({
        queryKey: courseKeys.all,
        queryFn: async () => {
            const data = await courseApi.getAll();
            return Array.isArray(data) ? data : (data?.data ?? []);
        },
    });

export const useCreateCourse = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: courseApi.create,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: courseKeys.all });
        },
    });
};

export const useUpdateCourse = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: courseApi.update,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: courseKeys.all });
        },
    });
};

export const useDeleteCourse = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: courseApi.delete,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: courseKeys.all });
        },
    });
};
