import axiosInstance from '../../../shared/api/axiosInstance';

export const authApi = {
    login: async (phoneNumber, password) => {
        const response = await axiosInstance.post('/auth/login', { phoneNumber, password });
        return response.data;
    },
};