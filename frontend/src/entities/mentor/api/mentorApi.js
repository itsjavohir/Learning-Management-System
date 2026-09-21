import axiosInstance from '../../../shared/api/axiosInstance';

export const mentorApi = {
    getAll: async () => {
        const response = await axiosInstance.get('/mentors');
        return response.data;
    },

    getById: async (id) => {
        const response = await axiosInstance.get(`/mentors/${id}`);
        return response.data;
    },

    getProfile: async () => {
        const response = await axiosInstance.get('/mentors/profile');
        return response.data;
    },

    updateProfile: async (profileData) => {
        const response = await axiosInstance.put('/mentors/profile', profileData);
        return response.data;
    },
};
