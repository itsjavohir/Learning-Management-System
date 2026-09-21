import { useMutation, useQueryClient } from '@tanstack/react-query';
import { mentorApi, mentorKeys } from '../../../../entities/mentor';

export const useUpdateMentorProfile = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: mentorApi.updateProfile,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: mentorKeys.profile });
        },
    });
};
