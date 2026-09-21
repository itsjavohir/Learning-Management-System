import { useQuery } from '@tanstack/react-query';
import { mentorApi } from '../api/mentorApi';

export const mentorKeys = {
    all: ['mentors'],
    profile: ['mentors', 'profile'],
};

export const useMentors = () =>
    useQuery({
        queryKey: mentorKeys.all,
        queryFn: async () => {
            const data = await mentorApi.getAll();
            return Array.isArray(data) ? data : (data?.data ?? []);
        },
    });

export const useMentorProfile = () =>
    useQuery({
        queryKey: mentorKeys.profile,
        queryFn: mentorApi.getProfile,
    });
