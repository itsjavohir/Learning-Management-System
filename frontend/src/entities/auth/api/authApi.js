import axiosInstance from '../../../shared/api/axiosInstance';

export const authApi = {
    login: async (phoneNumber, password) => {
        const response = await axiosInstance.post('/auth/login', { phoneNumber, password });
        return response.data;
    },

    changePassword: async (oldPassword, newPassword, confirmPassword) => {
        const response = await axiosInstance.post(
            '/auth/change-password',
            {
                oldPassword,
                newPassword,
                confirmPassword
            }
        );

        return response.data;
    },

    forgotPassword: async (phoneNumber) => {
        const response = await axiosInstance.post('/auth/forgot-password', { phoneNumber });
        return response.data;
    },

    resetPassword: async ({ phoneNumber, verifyCode, newPassword, confirmPassword }) => {
        const response = await axiosInstance.post('/auth/reset-password', {
            phoneNumber,
            verifyCode,
            newPassword,
            confirmPassword,
        });

        return response.data;
    },
};